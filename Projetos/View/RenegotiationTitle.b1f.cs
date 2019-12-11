using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;
using SAPbobsCOM;
using SAPbouiCOM;
using Model;

using System.Globalization;

namespace Projeto.View
{
    [FormAttribute("Projeto.View.TESTE2", "View/RenegotiationTitle.b1f")]
    class RenegotiationTitle : UserFormBase
    {
        public static List<Linha> LinhaTable { get; set; }

        public RenegotiationTitle()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("etCodePN").Specific));
            this.EditText6.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText6_ChooseFromListAfter);
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button2.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button2_PressedAfter);
            this.Button2.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.Button2_PressedBefore);
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("etName").Specific));
            this.EditText7.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText7_ChooseFromListAfter);
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.EditText10 = ((SAPbouiCOM.EditText)(this.GetItem("Item_5").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_10").Specific));
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("mtSum").Specific));
            this.Matrix0.PressedAfter += new SAPbouiCOM._IMatrixEvents_PressedAfterEventHandler(this.Matrix0_PressedAfter);
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_23").Specific));
            this.EditText18 = ((SAPbouiCOM.EditText)(this.GetItem("Item_25").Specific));
            this.LinkedButton1 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_12").Specific));
            this.StaticText11 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_13").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("etVlrTotal").Specific));
            this.StaticText13 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_19").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("etJuro").Specific));
            this.EditText4.LostFocusAfter += new SAPbouiCOM._IEditTextEvents_LostFocusAfterEventHandler(this.EditText4_LostFocusAfter);
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("etMulta").Specific));
            this.EditText5.LostFocusAfter += new SAPbouiCOM._IEditTextEvents_LostFocusAfterEventHandler(this.EditText5_LostFocusAfter);
            this.StaticText15 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_28").Specific));
            this.StaticText17 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_31").Specific));
            this.StaticText18 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_33").Specific));
            this.EditText11 = ((SAPbouiCOM.EditText)(this.GetItem("Item_34").Specific));
            this.StaticText19 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_35").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("etJuros").Specific));
            this.EditText0.LostFocusAfter += new SAPbouiCOM._IEditTextEvents_LostFocusAfterEventHandler(this.EditText0_LostFocusAfter);
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_32").Specific));
            this.EditText1.LostFocusAfter += new SAPbouiCOM._IEditTextEvents_LostFocusAfterEventHandler(this.EditText1_LostFocusAfter);
            this.cmbFilial = ((SAPbouiCOM.ComboBox)(this.GetItem("cmbFilial").Specific));
            this.cmbFilial.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.ComboBox0_ComboSelectAfter);
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_9").Specific));
            this.EditText3.ValidateBefore += new SAPbouiCOM._IEditTextEvents_ValidateBeforeEventHandler(this.EditText3_ValidateBefore);
            this.EditText8 = ((SAPbouiCOM.EditText)(this.GetItem("etDocDate").Specific));
            this.EditText8.LostFocusAfter += new SAPbouiCOM._IEditTextEvents_LostFocusAfterEventHandler(this.EditText8_LostFocusAfter);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_1").Specific));
            this.Button1.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button1_PressedAfter);
            this.OnCustomInitialize();
        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {

        }

        private void OnCustomInitialize()
        {
            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");
            var dtMatriz = UIAPIRawForm.DataSources.DataTables.Item("dtMatriz");

            Conditions conditions;
            Condition condition;

            conditions = new Conditions();
            condition = conditions.Add();
            condition.Alias = "CardType";
            condition.Operation = BoConditionOperation.co_EQUAL;
            condition.CondVal = "C";
            condition.Relationship = BoConditionRelationship.cr_AND;

            condition = conditions.Add();
            condition.Alias = "frozenFor";
            condition.Operation = BoConditionOperation.co_EQUAL;
            condition.CondVal = "N";

            var cflCardCode = UIAPIRawForm.ChooseFromLists.Item("cflCardCode");
            cflCardCode.SetConditions(conditions);

            var cflCardName = UIAPIRawForm.ChooseFromLists.Item("cflCardName");
            cflCardName.SetConditions(conditions);

            UIAPIRawForm.Freeze(true);
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
            dados.SetValue("U_Filial", dados.Offset, cmbFilial.ValidValues.Item(0).Value);
            dados.SetValue("U_Cnpj", dados.Offset, GetCnpjFilial(cmbFilial.ValidValues.Item(0).Value));

            Matrix0.AutoResizeColumns();
            UIAPIRawForm.Freeze(false);
        }

        //Campo nome
        private void EditText7_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Matrix0.AutoResizeColumns();

            if (UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_FIND_MODE) return;

            var dataTable = ((SAPbouiCOM.SBOChooseFromListEventArg)pVal).SelectedObjects;
            if (dataTable == null)
            {
                return;
            }

            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");
            var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            var dtMatriz = UIAPIRawForm.DataSources.DataTables.Item("dtMatriz");

            LinhaTable = new List<Linha>();

            dados.SetValue("U_Filial", dados.Offset, cmbFilial.ValidValues.Item(0).Value);
            dados.SetValue("U_Cnpj", dados.Offset, GetCnpjFilial(cmbFilial.ValidValues.Item(0).Value));
            dados.SetValue("U_CardCode", dados.Offset, dataTable.GetValue("CardCode", 0).ToString());
            dados.SetValue("U_CardName", dados.Offset, dataTable.GetValue("CardName", 0).ToString());
            dados.SetValue("U_DocDueDate", dados.Offset, DateTime.Now.ToString("yyyyMMdd"));
            dados.SetValue("U_DocDate", dados.Offset, DateTime.Now.ToString("yyyyMMdd"));
            CleanField();

            dtMatriz.Rows.Clear();
            GetBoletosEmAberto();

            Matrix0.LoadFromDataSourceEx();

        }

        //Campo code
        private void EditText6_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Matrix0.AutoResizeColumns();

            if (UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_FIND_MODE) return;

            var dataTable = ((SAPbouiCOM.SBOChooseFromListEventArg)pVal).SelectedObjects;
            if (dataTable == null)
            {
                return;
            }

            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");
            var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            var dtMatriz = UIAPIRawForm.DataSources.DataTables.Item("dtMatriz");

            LinhaTable = new List<Linha>();

            dados.SetValue("U_Filial", dados.Offset, cmbFilial.ValidValues.Item(0).Value);
            dados.SetValue("U_Cnpj", dados.Offset, GetCnpjFilial(cmbFilial.ValidValues.Item(0).Value));
            dados.SetValue("U_CardCode", dados.Offset, dataTable.GetValue("CardCode", 0).ToString());
            dados.SetValue("U_CardName", dados.Offset, dataTable.GetValue("CardName", 0).ToString());
            dados.SetValue("U_DocDueDate", dados.Offset, DateTime.Now.ToString("yyyyMMdd"));
            dados.SetValue("U_DocDate", dados.Offset, DateTime.Now.ToString("yyyyMMdd"));
            CleanField();

            dtMatriz.Rows.Clear();
            GetBoletosEmAberto();

            Matrix0.LoadFromDataSourceEx();
        }

        //Campo juros %
        private void EditText4_LostFocusAfter(object sboObject, SBOItemEventArg pVal)
        {
            var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");

            var txJuros = (double.Parse(dados.GetValue("U_JuroPrcnt", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture) * double.Parse(dados.GetValue("U_VlrTotal", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture) / 100);

            dados.SetValue("U_Juros", dados.Offset, (txJuros).ToString(CultureInfo.InvariantCulture));
            SetVlrTotal();
        }

        //Multas %
        private void EditText5_LostFocusAfter(object sboObject, SBOItemEventArg pVal)
        {
            var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");

            var txMulta = (double.Parse(dados.GetValue("U_MultaPrcnt", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture) * double.Parse(dados.GetValue("U_VlrTotal", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture) / 100);

            dados.SetValue("U_Multa", dados.Offset, (txMulta).ToString(CultureInfo.InvariantCulture));
            SetVlrTotal();
        }

        //Data de lançamento
        private void EditText8_LostFocusAfter(object sboObject, SBOItemEventArg pVal)
        {
            GetBoletosEmAberto();
        }

        //Campo do etJuros
        private void EditText0_LostFocusAfter(object sboObject, SBOItemEventArg pVal)
        {
            var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");

            var txJuros = 100 * double.Parse(dados.GetValue("U_Juros", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture);
            var calculo = txJuros / double.Parse(dados.GetValue("U_VlrTotal", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture);

            dados.SetValue("U_JuroPrcnt", dados.Offset, (calculo).ToString(CultureInfo.InvariantCulture));
            SetVlrTotal();
        }

        private void EditText3_ValidateBefore(object sboObject, SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");
            var dtMatriz = UIAPIRawForm.DataSources.DataTables.Item("dtMatriz");

            if (double.Parse(dados.GetValue("U_DocDueDate", dados.Offset)) < double.Parse(dados.GetValue("U_DocDate", dados.Offset)))
            {
                SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("No campo \"Data de vencimento\", entrar uma data igual ou posterior à data de lançamento  [Mensagem 439-41]", BoMessageTime.bmt_Short);
                BubbleEvent = false;
            }
        }

        //Campo etMulta
        private void EditText1_LostFocusAfter(object sboObject, SBOItemEventArg pVal)
        {
            var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");

            var txMulta = 100 * double.Parse(dados.GetValue("U_Multa", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture);
            var calculo = txMulta / double.Parse(dados.GetValue("U_VlrTotal", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture);

            dados.SetValue("U_MultaPrcnt", dados.Offset, (calculo).ToString(CultureInfo.InvariantCulture));
            SetVlrTotal();
        }

        private void ComboBox0_ComboSelectAfter(object sboObject, SBOItemEventArg pVal)
        {
            UIAPIRawForm.Freeze(true);

            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");
            var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            var dtMatriz = UIAPIRawForm.DataSources.DataTables.Item("dtMatriz");

            var query = @"SELECT TaxIdNum from OBPL 
                                 WHERE BPLId = {0}";

            recordset.DoQuery(String.Format(query, dados.GetValue("U_Filial", 0)));
            dados.SetValue("U_Cnpj", dados.Offset, recordset.Fields.Item("TaxIdNum").Value.ToString());

            CleanField();

            dtMatriz.Rows.Clear();
            GetBoletosEmAberto();
            UIAPIRawForm.Freeze(false);
        }

        private void Button2_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");
            var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
            var dtMatriz = UIAPIRawForm.DataSources.DataTables.Item("dtMatriz");
            var linhaObj = new Linha();

            Matrix0.FlushToDataSource();

            for (var i = linha.Size - 1; i >= 0; i--)
            {
                linha.RemoveRecord(i);
            }

            var count = 0;

            foreach (var line in LinhaTable)
            {
                linha.InsertRecord(count);

                linha.SetValue("U_NConta", count, line.NConta.ToString());
                linha.SetValue("U_NBoleto", count, line.NBoleto.ToString());
                linha.SetValue("U_Vencimento", count, line.Vencimento.ToString("yyyyMMdd"));
                linha.SetValue("U_Lancamento", count, line.Vencimento.ToString("yyyyMMdd"));
                linha.SetValue("U_Atraso", count, line.Atraso.ToString());
                linha.SetValue("U_ValorTitulo", count, line.ValorTitulo.ToString());
                count++;
            }

            if (double.Parse(dados.GetValue("U_VlrTotal", dados.Offset)) == 0)
            {
                BubbleEvent = false;

                SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Não é possível adicionar pagamento sem documentos  [Mensagem 3524-97]", BoMessageTime.bmt_Short);
            }
        }

        private void Matrix0_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            UIAPIRawForm.Freeze(true);
            try
            {
                if (pVal.ColUID != "Select") return;

                var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");
                var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
                var dtMatriz = UIAPIRawForm.DataSources.DataTables.Item("dtMatriz");

                Matrix0.FlushToDataSource();
                
                if (dtMatriz.GetValue("Select", pVal.Row - 1).ToString() == "Y")
                {
                    LinhaTable.Add(new Linha()
                    {
                        NConta = (int)dtMatriz.GetValue("NConta", pVal.Row - 1),
                        NBoleto = (int)dtMatriz.GetValue("NBoleto", pVal.Row - 1),
                        ValorTitulo = (double)dtMatriz.GetValue("ValorTitulo", pVal.Row - 1),
                        Vencimento = (DateTime)dtMatriz.GetValue("Vencimento", pVal.Row - 1),
                        Lancamento = (DateTime)dtMatriz.GetValue("Lancamento", pVal.Row - 1),
                        Atraso = (int)dtMatriz.GetValue("Atraso", pVal.Row - 1),
                        RowNumber = pVal.Row 
                    });
                }
                else
                {
                    var index = LinhaTable.FindIndex(x => x.RowNumber == pVal.Row);
                    LinhaTable.RemoveAt(index);
                }

                double total = LinhaTable.Sum(x => x.ValorTitulo);
                dados.SetValue("U_VlrTotal", dados.Offset, total.ToString(CultureInfo.InvariantCulture));

                Matrix0.LoadFromDataSourceEx();
                SetJuros();
                SetMulta();
                SetVlrTotal();
                UIAPIRawForm.Freeze(false);
            }
            catch
            {
                UIAPIRawForm.Freeze(false);
            }
        }

        //Campo selecionar todos;
        private void Button1_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            UIAPIRawForm.Freeze(true);
            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");
            var dtMatriz = UIAPIRawForm.DataSources.DataTables.Item("dtMatriz");

            if (double.Parse(dados.GetValue("U_VlrTotal", dados.Offset)) == 0)
            {
                for (int i = 0; i < dtMatriz.Rows.Count; i++)
                {
                    dtMatriz.SetValue("Select", i, "Y");

                    LinhaTable.Add(new Linha()
                    {
                        NConta = (int)dtMatriz.GetValue("NConta", i),
                        NBoleto = (int)dtMatriz.GetValue("NBoleto", i),
                        ValorTitulo = (double)dtMatriz.GetValue("ValorTitulo", i),
                        Vencimento = (DateTime)dtMatriz.GetValue("Vencimento", i),
                        Lancamento = (DateTime)dtMatriz.GetValue("Lancamento", i),
                        Atraso = (int)dtMatriz.GetValue("Atraso", i),
                        RowNumber = i + 1
                    });
                }
     
                Matrix0.LoadFromDataSourceEx();
            }
            else 
            {
                for (int i = 0; i < dtMatriz.Rows.Count; i++)
                {
                    if (dtMatriz.GetValue("Select", i).ToString() != "Y")
                    {
                        dtMatriz.SetValue("Select", i, "Y");
                        LinhaTable.Add(new Linha()
                        {
                            NConta = (int)dtMatriz.GetValue("NConta", i),
                            NBoleto = (int)dtMatriz.GetValue("NBoleto", i),
                            ValorTitulo = (double)dtMatriz.GetValue("ValorTitulo", i),
                            Vencimento = (DateTime)dtMatriz.GetValue("Vencimento", i),
                            Lancamento = (DateTime)dtMatriz.GetValue("Lancamento", i),
                            Atraso = (int)dtMatriz.GetValue("Atraso", i),
                            RowNumber = i + 1
                        });
                    }
                }
            }

            double total = LinhaTable.Sum(x => x.ValorTitulo);
            dados.SetValue("U_VlrTotal", dados.Offset, total.ToString(CultureInfo.InvariantCulture));
            SetJuros();
            SetMulta();
            SetVlrTotal();
            Matrix0.LoadFromDataSourceEx();

            UIAPIRawForm.Freeze(false);
        }

        //Campo desmarcar todos;
        private void Button0_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            UIAPIRawForm.Freeze(true);

            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");
            var dtMatriz = UIAPIRawForm.DataSources.DataTables.Item("dtMatriz");

            for (int i = 0; i < dtMatriz.Rows.Count; i++)
            {
                dtMatriz.SetValue("Select", i, "N");
            }

            LinhaTable = new List<Linha>();

            CleanField();
            Matrix0.LoadFromDataSourceEx();
            UIAPIRawForm.Freeze(false);
        }

        //Botão OK
        private void Button2_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            UIAPIRawForm.Freeze(true);

            if (UIAPIRawForm.Mode != BoFormMode.fm_ADD_MODE) return;

            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");
            var dtMatriz = UIAPIRawForm.DataSources.DataTables.Item("dtMatriz");

            var query = @"SELECT OBPL.BPLId, OBPL.BPLName, ORCT.VatRegNum
                             from OBPL 
	                         inner join ORCT on ORCT.BPLId = ORCT.DocEntry";

            recordset.DoQuery(query);

            dados.SetValue("U_Filial", dados.Offset, cmbFilial.ValidValues.Item(0).Value);
            dados.SetValue("U_Cnpj", dados.Offset, GetCnpjFilial(cmbFilial.ValidValues.Item(0).Value));
            dados.SetValue("U_DocDueDate", dados.Offset, DateTime.Now.ToString("yyyyMMdd"));
            dados.SetValue("U_DocDate", dados.Offset, DateTime.Now.ToString("yyyyMMdd"));

            Matrix0.AutoResizeColumns();
            UIAPIRawForm.Freeze(false);

        }

        private void SetJuros()
        {
            var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");

            var txJuros = (double.Parse(dados.GetValue("U_JuroPrcnt", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture) * double.Parse(dados.GetValue("U_VlrTotal", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture) / 100);

            dados.SetValue("U_Juros", dados.Offset, (txJuros).ToString(CultureInfo.InvariantCulture));
        }

        private void SetMulta()
        {
            var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");

            var txMulta = (double.Parse(dados.GetValue("U_MultaPrcnt", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture) * double.Parse(dados.GetValue("U_VlrTotal", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture) / 100);

            dados.SetValue("U_Multa", dados.Offset, (txMulta).ToString(CultureInfo.InvariantCulture));
        }

        private void SetVlrTotal()
        {
            var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");

            var soma = double.Parse(dados.GetValue("U_Juros", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture) + double.Parse(dados.GetValue("U_Multa", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture);
            var calculoVlrTotal = soma + double.Parse(dados.GetValue("U_VlrTotal", dados.Offset), NumberStyles.Currency, CultureInfo.InvariantCulture);

            dados.SetValue("U_TotalFinal", dados.Offset, (calculoVlrTotal).ToString(CultureInfo.InvariantCulture));
        }

        private void GetBoletosEmAberto()
        {
            UIAPIRawForm.Freeze(true);
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");
            var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
            var dtMatriz = UIAPIRawForm.DataSources.DataTables.Item("dtMatriz");
            dtMatriz.Rows.Clear();

            //Aparecer os titulos em aberto por padrão 
            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            var query = @"select distinct ORCT.BPLName, ORCT.VATRegNum, DocNum, OBOE.BoeNum, OBOE.BoeSum, DocDueDate, DocDate,
                                datediff(day, DocDueDate, getDate()) as Date
                                from ORCT
                            inner join OBOE ON OBOE.BoeKey = ORCT.DocEntry
                                 where Canceled = 'N'
                                 and ORCT.BPLId = {0}
                                 and ORCT.CardCode = '{1}'
                                 and ORCT.DocDate <= '{2}'";

            recordset.DoQuery(String.Format(query, dados.GetValue("U_Filial", dados.Offset).ToString(), dados.GetValue("U_CardCode", dados.Offset).ToString(), dados.GetValue("U_DocDate", dados.Offset).ToString()));
            
            dtMatriz.Rows.Add(recordset.RecordCount);

            var i = 0;
            while (!recordset.EoF)
            {
                dtMatriz.SetValue("NConta", i, recordset.Fields.Item("DocNum").Value.ToString());
                dtMatriz.SetValue("NBoleto", i, recordset.Fields.Item("BoeNum").Value.ToString());
                dtMatriz.SetValue("ValorTitulo", i, ((double)recordset.Fields.Item("BoeSum").Value).ToString(CultureInfo.InvariantCulture));
                dtMatriz.SetValue("Vencimento", i, DateTime.Parse(recordset.Fields.Item("DocDueDate").Value.ToString()).ToString("yyyyMMdd"));
                dtMatriz.SetValue("Lancamento", i, DateTime.Parse(recordset.Fields.Item("DocDate").Value.ToString()).ToString("yyyyMMdd"));
                dtMatriz.SetValue("Atraso", i, recordset.Fields.Item("Date").Value.ToString());
                recordset.MoveNext();
                i++;
            }

            UIAPIRawForm.Freeze(false);

            Matrix0.LoadFromDataSourceEx();
        }

        private string GetCnpjFilial(string BPLId)
        {
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");
            var linha = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_NGN1");
            var recordset = (Recordset)CommonController.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            var dtMatriz = UIAPIRawForm.DataSources.DataTables.Item("dtMatriz");

            var query = @"SELECT OBPL.BPLId, OBPL.BPLName, ORCT.VatRegNum
                             from OBPL 
	                         inner join ORCT on ORCT.BPLId = ORCT.DocEntry
                           	 WHERE OBPL.BPLId = {0}";

            recordset.DoQuery(String.Format(query, BPLId));

            return recordset.Fields.Item("VatRegNum").Value.ToString();
        }

        private void CleanField()
        {
            var dados = UIAPIRawForm.DataSources.DBDataSources.Item("@HTT_ONGN");
            dados.SetValue("U_TotalFinal", dados.Offset, (0).ToString(CultureInfo.InvariantCulture));
            dados.SetValue("U_Juros", dados.Offset, (0).ToString(CultureInfo.InvariantCulture));
            dados.SetValue("U_Multa", dados.Offset, (0).ToString(CultureInfo.InvariantCulture));
            dados.SetValue("U_VlrTotal", dados.Offset, (0).ToString(CultureInfo.InvariantCulture));
            dados.SetValue("U_Obs", dados.Offset, ("").ToString());
        }

        private SAPbouiCOM.EditText EditText6;
        private SAPbouiCOM.Button Button2;
        private SAPbouiCOM.EditText EditText7;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText EditText10;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.Matrix Matrix0;
        private SAPbouiCOM.StaticText StaticText9;
        private SAPbouiCOM.EditText EditText18;
        private LinkedButton LinkedButton1;
        private EditText EditText1;
        private StaticText StaticText11;
        private EditText EditText2;
        private StaticText StaticText13;
        private EditText EditText4;
        private EditText EditText5;
        private StaticText StaticText15;
        private StaticText StaticText17;
        private StaticText StaticText18;
        private EditText EditText11;
        private StaticText StaticText19;
        private EditText EditText0;
        private ComboBox cmbFilial;
        private EditText EditText3;
        private EditText EditText8;
        private Button Button0;
        private Button Button1;
    }
}
