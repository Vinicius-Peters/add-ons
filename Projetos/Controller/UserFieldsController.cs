using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projeto.Controller
{
    public class UserFieldsController
    {
        public static bool HaveLikedTable(string tableName, string fieldName)
        {
            var userFieldsMD = (UserFieldsMD)CommonController.Company.GetBusinessObject(BoObjectTypes.oUserFields);
            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);

            var query = @"select CUFD.RTable
                             from CUFD
                            where CUFD.TableID = '{0}'
                              and CUFD.AliasID = '{1}'";

            recordset.DoQuery(String.Format(query, tableName, fieldName));

            if (recordset.RecordCount > 0)
            {
                return !String.IsNullOrEmpty(recordset.Fields.Item("RTable").Value.ToString());
            }

            return false;
        }

        public static string GetLikedTableValues(string tableName, string fieldName)
        {
            var validValues = new Dictionary<string, string>();
            var userFieldsMD = (UserFieldsMD)CommonController.Company.GetBusinessObject(BoObjectTypes.oUserFields);
            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);

            CommonController.Company.XMLAsString = true;
            CommonController.Company.XmlExportType = BoXmlExportTypes.xet_ValidNodesOnly;

            var query = $@"declare @query nvarchar(max);
                           declare @tableName nvarchar(max);
                           select @tableName = CUFD.""RTable""
                             from CUFD
                            where CUFD.""TableID"" = '{tableName}'
                              and CUFD.""AliasID"" = '{fieldName}'
                              set @query = 'select '''' ""Code"", '''' ""Name"" union all select ""Code"", ""Name"" from ""@' + @tableName + '"" order by ""Code""'
                             exec(@query)";

            recordset.DoQuery(query);

            var xml = String.Empty;

            recordset.SaveXML(ref xml);

            return xml;
        }
    }
}
