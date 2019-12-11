using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;
using SAPbobsCOM;
using SAPbouiCOM;
using System.Globalization;
using Model;

namespace Projeto.View
{
    [FormAttribute("Projeto.View.Cobrança_Avulsa", "View/CobrancaAvulsa.b1f")]
    class Cobrança_Avulsa : UserFormBase
    {

        public Cobrança_Avulsa()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.etCode = ((SAPbouiCOM.EditText)(this.GetItem("etCode").Specific));
            this.etCode.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.etCode_ChooseFromListAfter);
            this.mtItens = ((SAPbouiCOM.Matrix)(this.GetItem("mtItens").Specific));
            this.EditText10 = ((SAPbouiCOM.EditText)(this.GetItem("etName").Specific));
            this.EditText10.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText10_ChooseFromListAfter);
            this.LinkedButton0 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_21").Specific));
            this.cmbFilial = ((SAPbouiCOM.ComboBox)(this.GetItem("cmbFilial").Specific));
            this.cmbFilial.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.cmbFilial_ComboSelectAfter);
            this.mtItens.ChooseFromListBefore += new SAPbouiCOM._IMatrixEvents_ChooseFromListBeforeEventHandler(this.mtItens_ChooseFromListBefore);
            this.mtItens.ValidateAfter += new SAPbouiCOM._IMatrixEvents_ValidateAfterEventHandler(this.mtItens_ValidateAfter);
            this.mtItens.ChooseFromListAfter += new SAPbouiCOM._IMatrixEvents_ChooseFromListAfterEventHandler(this.mtItens_ChooseFromListAfter);
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("Item_10").Specific));
            this.Button2.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button2_PressedAfter);
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_13").Specific));
            this.Button3 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button3.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.Button3_PressedBefore);
            this.Button3.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button3_PressedAfter);
            this.etLanc = ((SAPbouiCOM.EditText)(this.GetItem("etLanc").Specific));
            this.etVenc = ((SAPbouiCOM.EditText)(this.GetItem("etVenc").Specific));
            this.etDoc = ((SAPbouiCOM.EditText)(this.GetItem("etDoc").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_9").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("etExtract").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_14").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_15").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_19").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_20").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_23").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_16").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("dbDataBrow").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.DataAddBefore += new SAPbouiCOM.Framework.FormBase.DataAddBeforeHandler(this.Form_DataAddBefore);
            this.DataUpdateBefore += new SAPbouiCOM.Framework.FormBase.DataUpdateBeforeHandler(this.Form_DataUpdateBefore);
            this.DataLoadAfter += new DataLoadAfterHandler(this.Form_DataLoadAfter);
        }

        private void OnCustomInitialize()
        {
            mtItens.AutoResizeColumns();
            var olcn = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");
            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            var udDocEntry = UIAPIRawForm.DataSources.UserDataSources.Item("udDocEntry");

            var consulta = @"select coalesce(max(DocEntry), 0) + 1 as DocEntry
                            from[@HTT_OLCN]";

            recordset.DoQuery(consulta);

            udDocEntry.Value = recordset.Fields.Item("DocEntry").Value.ToString();

            Conditions conditions;
            Condition condition;

            //Condições para aparecer somente Clientes.
            conditions = new Conditions();
            condition = conditions.Add();
            condition.Alias = "CardType";
            condition.Operation = BoConditionOperation.co_EQUAL;
            condition.CondVal = "C";
            condition.Relationship = BoConditionRelationship.cr_AND;
            
            //Condições para aparecer somente Clientes Ativos.
            condition = conditions.Add();
            condition.Alias = "frozenFor";
            condition.Operation = BoConditionOperation.co_EQUAL;
            condition.CondVal = "N";

            var cflCardCode = UIAPIRawForm.ChooseFromLists.Item("cflCardCode");
            cflCardCode.SetConditions(conditions);

            var cflCardName = UIAPIRawForm.ChooseFromLists.Item("cflCardName");
            cflCardName.SetConditions(conditions);

            conditions = new Conditions();
            condition = conditions.Add();
            condition.Alias = "frozenFor";
            condition.Operation = BoConditionOperation.co_EQUAL;
            condition.CondVal = "N";

            var cflItemCode = UIAPIRawForm.ChooseFromLists.Item("cflItemCode");
            cflItemCode.SetConditions(conditions);

            var cflItemName = UIAPIRawForm.ChooseFromLists.Item("cflItemName");
            cflItemName.SetConditions(conditions);

            var query = @"SELECT OBPL.BPLId, OBPL.BPLName, ORCT.VatRegNum
                             from OBPL 
	                         inner join ORCT on ORCT.BPLId = ORCT.DocEntry";

            recordset.DoQuery(query);

            while (!recordset.EoF)
            {
                cmbFilial.ValidValues.Add(recordset.Fields.Item("BPLId").Value.ToString(), recordset.Fields.Item("BPLName").Value.ToString());

                recordset.MoveNext();
            }

            cmbFilial.ExpandType = BoExpandType.et_DescriptionOnly;
            olcn.SetValue("U_BPLId", olcn.Offset, cmbFilial.ValidValues.Item(0).Value);
            olcn.SetValue("U_BPLName", olcn.Offset, GetCnpjFilial(cmbFilial.ValidValues.Item(0).Value));

            olcn.SetValue("U_DocDate", olcn.Offset, DateTime.Now.ToString("yyyyMMdd"));
            olcn.SetValue("U_DocDueDate", olcn.Offset, DateTime.Now.ToString("yyyyMMdd"));
            olcn.SetValue("U_DocTaxDate", olcn.Offset, DateTime.Now.ToString("yyyyMMdd"));

            var query2 = @"select max(DocEntry) + 1 as DocEntry 
                            from [@HTT_OLCN]";

            recordset.DoQuery(query2);

            olcn.SetValue("DocEntry", olcn.Offset, recordset.Fields.Item("DocEntry").Value.ToString());

            mtItens.AutoResizeColumns();
        }

        //Campo código   
        private void etCode_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            var olcn = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");
            var lcn1 = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_LCN1");

            if (UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_FIND_MODE) return;

            var dataTable = ((SAPbouiCOM.SBOChooseFromListEventArg)pVal).SelectedObjects;
            if (dataTable == null)
            {
                return;
            }

            if (String.IsNullOrEmpty(lcn1.GetValue("U_ItemCode", 0)))
            {
                lcn1.InsertRecord(1);
                lcn1.RemoveRecord(1);
            }
            else
            {
                lcn1.InsertRecord(lcn1.Size);
            }

            olcn.SetValue("U_CardName", olcn.Offset, dataTable.GetValue("CardName", 0).ToString());
            olcn.SetValue("U_CardCode", olcn.Offset, dataTable.GetValue("CardCode", 0).ToString());

            mtItens.LoadFromDataSourceEx();
            mtItens.AutoResizeColumns();

        }

        //Nome do cliente
        private void EditText10_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_FIND_MODE) return;

            var dataTable = ((SAPbouiCOM.SBOChooseFromListEventArg)pVal).SelectedObjects;
            if (dataTable == null)
            {
                return;
            }

            var documento = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");

            documento.SetValue("U_CardCode", documento.Offset, dataTable.GetValue("CardCode", 0).ToString());
            documento.SetValue("U_CardName", documento.Offset, dataTable.GetValue("CardName", 0).ToString());
        }

        //cfl dos campos da matriz.
        private void mtItens_ChooseFromListAfter(object sboObject, SBOItemEventArg pVal)
        {
            var olcn = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");
            var lcn1 = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_LCN1");
            var dataTable = ((SAPbouiCOM.SBOChooseFromListEventArg)pVal).SelectedObjects;

            if (dataTable == null)
            {
                return;
            }

            mtItens.FlushToDataSource();
            CleanField(pVal);

            //Pega as datas dos campos de referência
            var dataInicial = DateTime.ParseExact(olcn.GetValue("U_FromDetDate", 0), "yyyyMMdd", null);
            var dataFinal = DateTime.ParseExact(olcn.GetValue("U_ToDetDate", 0), "yyyyMMdd", null);

            var dataConcatenada = dataInicial.ToShortDateString() + " a " + dataFinal.ToShortDateString();

            switch (pVal.ColUID)
            {
                case "ItemCode":
                    lcn1.SetValue("U_ItemDesc", pVal.Row - 1, dataTable.GetValue("ItemName", 0).ToString());
                    lcn1.SetValue("U_Period", pVal.Row - 1, dataConcatenada);
                    SumValues();
                    break;
                case "ItemName":
                    lcn1.SetValue("U_ItemCode", pVal.Row - 1, dataTable.GetValue("ItemCode", 0).ToString());
                    lcn1.SetValue("U_Period", pVal.Row - 1, dataConcatenada);
                    SumValues();
                    break;
            }

            if (pVal.Row == lcn1.Size)
            {
                lcn1.InsertRecord(lcn1.Size);
            }

            mtItens.LoadFromDataSourceEx();
        }

        private void mtItens_ChooseFromListBefore(object sboObject, SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            var olcn = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");
            var lcn1 = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_LCN1");

            if (String.IsNullOrEmpty(olcn.GetValue("U_FromDetDate", olcn.Offset)) || String.IsNullOrEmpty(olcn.GetValue("U_ToDetDate", olcn.Offset)))
            {
                BubbleEvent = false;
                SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Campo data de referência não informado. ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return;
            }
        }

        private string GetCnpjFilial(string BPLId)
        {
            var documento = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");
            var grid = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_LCN1");

            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);

            var query = @"SELECT OBPL.BPLId, OBPL.BPLName, ORCT.VatRegNum
                             from OBPL 
                            inner join ORCT on ORCT.BPLId = ORCT.DocEntry
                             WHERE OBPL.BPLId = {0}";

            recordset.DoQuery(String.Format(query, BPLId));

            return recordset.Fields.Item("VatRegNum").Value.ToString();
        }
        //Combo box filial
        private void cmbFilial_ComboSelectAfter(object sboObject, SBOItemEventArg pVal)
        {
            UIAPIRawForm.Freeze(true);

            var documento = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");
            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);

            var query = @"SELECT VatRegNum from ORCT 
                                 WHERE BPLId = {0}";

            recordset.DoQuery(String.Format(query, documento.GetValue("U_BPLId", 0)));
            documento.SetValue("U_BPLName", documento.Offset, recordset.Fields.Item("VatRegNum").Value.ToString());

            UIAPIRawForm.Freeze(false);
        }

        //Btn Imprimir
        private void Button2_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            var olcn = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");
            var extract = olcn.GetValue("U_Extrato", olcn.Offset);

            DebtSettElementDeclaration.PrintReport(int.Parse(extract));
        }

        //Btn OK
        private void Button3_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            UIAPIRawForm.Freeze(true);
            var olcn = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");
            var documento = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");
            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);

            documento.SetValue("U_DocDate", documento.Offset, DateTime.Now.ToString("yyyyMMdd"));
            documento.SetValue("U_DocDueDate", documento.Offset, DateTime.Now.ToString("yyyyMMdd"));
            documento.SetValue("U_DocTaxDate", documento.Offset, DateTime.Now.ToString("yyyyMMdd"));

            var query = @"SELECT OBPL.BPLId, OBPL.BPLName, ORCT.VatRegNum
                             from OBPL 
	                         inner join ORCT on ORCT.BPLId = ORCT.DocEntry";

            recordset.DoQuery(query);

            cmbFilial.ExpandType = BoExpandType.et_DescriptionOnly;
            var query2 = @"select max(DocEntry) + 1 as DocEntry 
                            from [@HTT_OLCN]";

            recordset.DoQuery(query2);

            documento.SetValue("U_BPLId", documento.Offset, cmbFilial.ValidValues.Item(0).Value);
            documento.SetValue("U_BPLName", documento.Offset, GetCnpjFilial(cmbFilial.ValidValues.Item(0).Value));
            documento.SetValue("U_DocNum", documento.Offset, recordset.Fields.Item("DocEntry").Value.ToString());

            mtItens.AutoResizeColumns();
            UIAPIRawForm.Freeze(false);
        }

        //Btn OK
        private void Button3_PressedBefore(object sboObject, SBOItemEventArg pVal, out bool BubbleEvent)
        {

            BubbleEvent = true;
            UIAPIRawForm.Freeze(true);

            if (UIAPIRawForm.Mode != BoFormMode.fm_ADD_MODE && UIAPIRawForm.Mode != BoFormMode.fm_UPDATE_MODE) return;
            var olcn = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");
            var documento = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");
            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            var grid = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_LCN1");

            grid.RemoveRecord(grid.Size - 1);

            mtItens.FlushToDataSource();

            if (Double.Parse(etVenc.Value) < Double.Parse(etLanc.Value))
            {
                BubbleEvent = false;
                SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Data de vencimento/lançamento inválida. ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return;
            }

            for (int i = 0; i < grid.Size - 1; i++)
            {
                if (Double.Parse(grid.GetValue("U_ItemVlr", i)) <= 0.0)
                {
                    SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Não é permitido campos em brancos!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    BubbleEvent = false;
                }
            }

            UIAPIRawForm.Freeze(false);
        }

        private void Form_DataAddBefore(ref BusinessObjectInfo pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            var grid = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_LCN1");

            for (int i = 0; i < grid.Size; i++)
            {
                if (double.Parse(grid.GetValue("U_ItemVlr", i)) == 0.0)
                {
                    grid.RemoveRecord(i);
                }
            }
        }

        private void Form_DataUpdateBefore(ref BusinessObjectInfo pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            var grid = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_LCN1");

            for (int i = 0; i < grid.Size; i++)
            {
                if (double.Parse(grid.GetValue("U_ItemVlr", i)) == 0.0)
                {
                    grid.RemoveRecord(i);
                }
            }
        }

        private void Form_DataLoadAfter(ref BusinessObjectInfo pVal)
        {
            var grid = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_LCN1");

            grid.InsertRecord(grid.Size);

            mtItens.LoadFromDataSourceEx();
        }

        //Valor total
        private void mtItens_ValidateAfter(object sboObject, SBOItemEventArg pVal)
        {
            if (pVal.ColUID != "mtValor") return;

            if (!pVal.ItemChanged) return;

            UIAPIRawForm.Freeze(true);
            var olcn = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");
            var lcn1 = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_LCN1");

            mtItens.FlushToDataSource();

            SumValues();

            mtItens.LoadFromDataSourceEx();
            UIAPIRawForm.Freeze(false);
        }

        private void CleanField(SBOItemEventArg pVal)
        {
            var olcn = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");
            var lcn1 = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_LCN1");

            lcn1.SetValue("U_ItemVlr", pVal.Row - 1, 0.ToString());
            lcn1.SetValue("U_ItemDesc", pVal.Row - 1, "");
            lcn1.SetValue("U_ItemDetails", pVal.Row - 1, "");
            lcn1.SetValue("U_Design", pVal.Row - 1, "");
            lcn1.SetValue("U_Period", pVal.Row - 1, "");
        }

        private void SumValues()
        {
            var olcn = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_OLCN");
            var lcn1 = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_LCN1");

            var total = 0.0;

            for (int i = 0; i < lcn1.Size - 1; i++)
            {
                total += double.Parse(lcn1.GetValue("U_ItemVlr", i), NumberStyles.Currency, CultureInfo.InvariantCulture);
            }

            olcn.SetValue("U_Total", olcn.Offset, total.ToString(CultureInfo.InvariantCulture));
            total = 0;
        }

        private EditText etDoc;
        private EditText etVenc;
        private SAPbouiCOM.EditText EditText10;
        private SAPbouiCOM.EditText etCode;
        private SAPbouiCOM.LinkedButton LinkedButton0;
        private ComboBox cmbFilial;
        private Button Button2;
        private StaticText StaticText9;
        private Button Button3;
        private EditText etLanc;
        private StaticText StaticText0;
        private EditText EditText0;
        private StaticText StaticText1;
        private EditText EditText1;
        private StaticText StaticText4;
        private EditText EditText3;
        private EditText EditText4;
        private StaticText StaticText2;
        private EditText EditText2;
        private Matrix mtItens;

    }
}
