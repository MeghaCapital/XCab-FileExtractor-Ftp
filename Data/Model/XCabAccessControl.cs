using Data.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
	public class XCabAccessControl
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string SharedKey { get; set; }
		public string? APIKey { get; set; }
		public bool UsesRestfulWebservice { get; set; }
		public string AccountCode { get; set; }
		public int StateId { get; set; }
		public bool Enabled { get; set; }
		public bool IsPostcodeOptional { get;  set; }
		public EAccessControl AccessVerification { get; set; }
	}
}
