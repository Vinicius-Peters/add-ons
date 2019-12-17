using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;
using SAPbobsCOM;
using System.Xml.XPath;
using System.Xml;
using System.Xml.Linq;
using Projeto.Controller;

namespace Projeto.View
{
    [FormAttribute("Projeto.View.ManipulationXML", "View/ManipulationXML.b1f")]
    class ManipulationXML : UserFormBase
    {
        public ManipulationXML()
        {

        }
        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.cmbBox = ((SAPbouiCOM.ComboBox)(this.GetItem("cmbBox").Specific));
            this.cmbBox.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.ComboBox0_ComboSelectAfter);
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_0").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {

        }

        private SAPbouiCOM.ComboBox cmbBox;

        private void OnCustomInitialize()
        {
            //Pegando o xml
            XmlDocument docBase = new XmlDocument();
            XmlDocument formXml = new XmlDocument();
            XmlDocument estudoXml = new XmlDocument();

            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ManipulationXML");
            //Carrega o arquivo
            docBase.Load(@"d:\Users\vinicius.peters\documents\visual studio 2015\Projects\Projeto\Projeto\View\Base.xml");

            formXml.InnerXml = UIAPIRawForm.GetAsXML();

            var nodes = formXml.SelectNodes("/Application/forms/action/form/items/action/item[@type='113']");

            //Armazena no xml Base os comboboxes cujos campos de usuário contenham uma tabela vinculada
            foreach (XmlNode node in formXml.SelectNodes("/Application/forms/action/form/items/action/item[@type='113']"))
            {
                var table = node.SelectSingleNode("specific/databind/@table").Value;
                var field = node.SelectSingleNode("specific/databind/@alias").Value;

                //Pega o resultado da consulta, e importa os nós 
                if (Controller.UserFieldsController.HaveLikedTable(table, field.Replace("U_", "")))
                {
                    var newNode = docBase.ImportNode(node, true);
                    docBase.SelectSingleNode("Application/forms/action/form/items/action").AppendChild(newNode);
                }
            }

            //Se o tipo do nó for diferente de null, ele pega o valor do type do formulário.
            var formTypeNode = docBase.DocumentElement.SelectSingleNode("/Application/forms/action/form/@FormType");
            if (formTypeNode != null)
            {
                formTypeNode.Value = UIAPIRawForm.TypeEx;
            }
            
            //Se o tipo do nó for diferente de null, ele pega o valor do type do formulário.
            var uidNode = docBase.DocumentElement.SelectSingleNode("/Application/forms/action/form/@uid");
            if (uidNode != null)
            {
                uidNode.Value = UIAPIRawForm.UniqueID;
            }

            // Insere os valores válidos nos comboboxes cujos campos de usuário contenham uma tabela vinculada
            foreach (XmlNode node in docBase.SelectNodes("Application/forms/action/form/items/action/item[@type='113']"))
            {
                var table = node.SelectSingleNode("specific/databind/@table").Value;
                var field = node.SelectSingleNode("specific/databind/@alias").Value;

                string resultadoDaConsulta = UserFieldsController.GetLikedTableValues(table, field.Replace("U_", ""));

                // Monta xml de valores válidos, obtendo-os no banco de dados
                XmlDocument docValidValues = new XmlDocument();
                docValidValues.LoadXml(resultadoDaConsulta);

                //Criar elemento action, e atributo =type="add"
                XmlElement elem = docValidValues.CreateElement("action");
                XmlAttribute attr = docValidValues.CreateAttribute("type");
                attr.Value = "add";

                //Seleciona o no especifico
                var no = node.SelectSingleNode("specific/ValidValues/action");
                var documento = docValidValues.InnerXml.Replace("<row>", "<ValidValue")
                                              .Replace("</row>", "")
                                              .Replace("<Code />", " value=\"\"")
                                              .Replace("<Name />", " description=\"\" />")
                                              .Replace("<Code>", " value=\"")
                                              .Replace("</Code>", "\"")
                                              .Replace("<Name>", " description =\"")
                                              .Replace("</Name>", "\"/>");

                //Carrega o documento corrigido
                docValidValues.LoadXml(documento);
                var elemStr = docValidValues.SelectNodes(@"BOM/BO/CUFD");
                
                //Faz a inserção no documento base.xml
                no.InnerXml = elemStr[0].InnerXml;

            }

            var innerXml = docBase.InnerXml;
            Application.SBO_Application.LoadBatchActions(ref innerXml);
        }

        private void ComboBox0_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

        }

        private SAPbouiCOM.ComboBox ComboBox0;
    }
}
