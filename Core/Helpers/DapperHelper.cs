using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Helpers
{
    public static class DapperHelper
    {
        /// <summary>
        /// Generates the search terms for a dapper query
        /// </summary>
        /// <param name="searchableFieldNames">all db fields to be searched</param>
        /// <param name="dbArgs"></param>
        /// <param name="q">the search terms</param>
        /// <param name="paramPrefix">Set a different prefix for each call if this method is called multiple times for 1 query. If it is called once, you do not need to set a prefix</param>
        /// <param name="exactMatch"></param>
        /// <returns></returns>w
        public static string AddSearchTerms(string[] searchableFieldNames, DynamicParameters dbArgs,
            string q, string paramPrefix = null, bool exactMatch = false)
        {
            var count = 0;
            var command = "";

            if (String.IsNullOrEmpty(q))
                return command;
            foreach (var token in q.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries))
            {
                var paramName = paramPrefix != null ? $"{paramPrefix}token{count}" : "token" + count;
                command += "    AND (";
                var fieldCount = 0;
                foreach (var colName in searchableFieldNames)
                {
                    if (fieldCount > 0)
                        command += " OR ";
                    command += GetSearchString(colName, paramName, exactMatch);
                    fieldCount++;
                }

                command += ")";
                dbArgs.Add(paramName, token);
                count++;
            }
            return command;

        }

        /*        public static SqlMapper.ICustomQueryParameter GenerateIdTableValuedParameter(int[] ids)
                {
                    var companyIdsDt = new DataTable();
                    companyIdsDt.Columns.Add("Id", typeof(int));
                    foreach (var id in ids)
                    {
                        companyIdsDt.Rows.Add(id);
                    }
                    return companyIdsDt.AsTableValuedParameter("IdType");
                }*/

        /* public static SqlMapper.ICustomQueryParameter GenerateIdPairingTableValuedParameter(ICollection<KeyValuePair<int, int>> idPairs)
         {
             var companyIdsDt = new DataTable();
             companyIdsDt.Columns.Add("Id1", typeof(int));
             companyIdsDt.Columns.Add("Id2", typeof(int));
             foreach (var pair in idPairs)
             {
                 companyIdsDt.Rows.Add(pair.Key, pair.Value);
             }
             return companyIdsDt.AsTableValuedParameter("IdPairingType");
         }*/

        private static string GetSearchString(string colName, string paramName, bool exactMatch)
        {
            return !exactMatch ? $"{colName} LIKE '%' + @{paramName} + '%'" : $"{colName} LIKE '' + @{paramName} + ''";
        }


        public static string GetOrderByClause(string s, string[] whiteList, string defaultSortString)
        {
            var sortFields = GetSortFields(s, whiteList);
            var useDefaultSortString = false;
            if (sortFields == null)
            {
                //couldnt sort. Use the default sort
                sortFields = GetSortFields(defaultSortString, whiteList);
                useDefaultSortString = true;
            }
            if (sortFields == null)
                return defaultSortString;

            var direction = GetSortDirection(!useDefaultSortString ? s : defaultSortString);
            var retOrderBy = sortFields.Aggregate("", (current, sort) => current + $"{sort} {direction}, ");
            return retOrderBy.Remove(retOrderBy.Length - 2);
        }
        private static IEnumerable<string> GetSortFields(string s, string[] whiteList)
        {
            if (String.IsNullOrEmpty(s))
                return null;
            //split by comma if you want to sort by multiple columns
            var sortColumns = s.Split(',').Select(x => x.Trim()).ToList();
            //split the last param by space. The last param should be in the <col-name> <sort-direction> format
            var lastSortParams = sortColumns.Last().Split(' ');
            if (lastSortParams.Length != 2 && lastSortParams.Length != 1)
                //remove the direction from the last entry
                sortColumns[sortColumns.Count - 1] = lastSortParams[0];

            var compareList = whiteList.Select(x => x.ToLower()).ToList();
            var retSortFields = new List<string>();
            var valid = true;
            foreach (var col in sortColumns)
            {
                if (!compareList.Contains(col.ToLower()))
                {
                    valid = false;
                    break;
                }
                retSortFields.Add(col);
            }
            if (!valid)
                return null;
            //return new [] {defaultSortString};

            //returnStr = returnStr.Remove(returnStr.Length - 2);
            return retSortFields;

            //return whiteList.Select(x => x.ToLower()).Contains(sortParts[0].ToLower()) ? sortParts[0] : defaultSortString;
            //return whiteList.Select(x => x.ToLower()).Contains(sortParts[0].ToLower()) ? sortParts[0] : defaultSortString;
        }

        private static string GetSortDirection(string s)
        {
            var sortParts = s.Split(' ');
            if (sortParts.Length < 2)
                return "asc";
            return sortParts[sortParts.Length - 1].Equals("desc", StringComparison.InvariantCultureIgnoreCase) ? "desc" : "asc";
        }

        /// <summary>
        /// Use this to generate a 'WHERE' clause for an Oracle query from a list of STRINGS.
        /// </summary>
        /// <param name="list">The list of Strings</param>
        /// <param name="whereIn">The column you are checking</param>
        /// <param name="useWhereNotAnd">To use WHERE, set to true. To use AND, set to false.</param>
        /// <returns></returns>
        public static string GenerateWhereListForOracle(List<string> list, string whereIn, bool useWhereNotAnd = true)
        {
            var joinedWhereClause = string.Join(" OR ",
                list.Batch(200)
                    .Select(batch => $@" {whereIn} IN ({string.Join(",", batch.Select(x => "'" + x + "'"))}) ")
                    .Select(x => x));

            var output = $@" { (useWhereNotAnd ? "WHERE" : "AND") } ({joinedWhereClause}) ";

            return output;
        }
        public static string GenerateWhereListForSqlServer(List<string> list, string whereIn, bool useWhereNotAnd = true)
        {
            var joinedWhereClause = string.Join(" OR ",
                list.Batch(200)
                    .Select(batch => $@" {whereIn} IN ({string.Join(",", batch.Select(x => "'" + x + "'"))}) ")
                    .Select(x => x));

            var output = $@" { (useWhereNotAnd ? "WHERE" : "AND") } ({joinedWhereClause}) ";

            return output;
        }
        /// <summary>
        /// Use this to generate a 'WHERE' clause for an Oracle query from a list of INTS.
        /// </summary>
        /// <param name="list">The list of Ints</param>
        /// <param name="whereIn">The column you are checking</param>
        /// <param name="useWhereNotAnd">To use WHERE, set to true. To use AND, set to false.</param>
        /// <returns></returns>
        public static string GenerateWhereListForOracle(List<int> list, string whereIn, bool useWhereNotAnd = true)
        {
            var joinedWhereClause = string.Join(" OR ",
                list.Batch(200)
                    .Select(batch => $@" {whereIn} IN ({string.Join(",", batch.Select(x => x))}) ")
                    .Select(x => x));

            var output = $@" { (useWhereNotAnd ? "WHERE" : "AND") } ({joinedWhereClause}) ";

            return output;
        }

    }
}
