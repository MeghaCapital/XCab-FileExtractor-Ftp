using System.IO;
using System.Text;

namespace Edifact_Library.parser.utils
{
    class StringWriterWithEncoding : StringWriter
    {
        public StringWriterWithEncoding(StringBuilder sb, Encoding encoding) : base(sb)
        {
            m_encoding = encoding;
        }
        public override System.Text.Encoding Encoding
        {
            get
            {
                return m_encoding;
            }
        }

        private Encoding m_encoding;
    }//StringWriterWithEncoding
}
