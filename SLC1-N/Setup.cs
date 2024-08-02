namespace SLC1_N
{
    public class Setup
    {
        public class Port
        {
            public string CH1ADCPort { get; set; }
            public string CH1ADCBaud { get; set; }
            public string CH1VDCPort { get; set; }
            public string CH1VDCBaud { get; set; }
            public string CH2ADCPort { get; set; }
            public string CH2VDCPort { get; set; }
            public string CH2ADCBaud { get; set; }
            public string CH2VDCBaud { get; set; }
            public string CodePort { get; set; }
            public string CodeBaud { get; set; }
            public string CH2CodePort { get; set; }
            public string CH2CodeBaud { get; set; }
            public string CH1FlowPort { get; set; }
            public string CH1FlowBaud { get; set; }
            public string CH2FlowPort { get; set; }
            public string CH2FlowBaud { get; set; }
            public string CH3FlowPort { get; set; }
            public string CH3FlowBaud { get; set; }
            public string CH4FlowPort { get; set; }
            public string CH4FlowBaud { get; set; }
            public string CKCH1Port { get; set; }
            public string CKCH1Baud { get; set; }
            public string CKCH2Port { get; set; }
            public string CKCH2Baud { get; set; }
        }

        public class Code_Setting
        {
            public string LeftCodeLength { get; set; }
            public string RightCodeLength { get; set; }
            public bool CHKCH1 { get; set; }
            public bool CHKCH2 { get; set; }
            public string CH1Code { get; set; }
            public string CH2Code { get; set; }
            public int LeftCodeLife { get; set; }
            public int RightCodeLife { get; set; }
        }
        public class Work_Order
        {
            public string ProductName { get; set; }
            public string ProductModel { get; set; }
            public string WorkOrder { get; set; }
            public string ProductionItem { get; set; }
            public string TestType { get; set; }
            public string TestStation { get; set; }
            public string ProductNum { get; set; }
        }
        public class Save
        {
            public bool ChkExcel { get; set; }
            public bool ChkMES { get; set; }
            public bool ChkCSV { get; set; }
            public string Path { get; set; }
            public string Mes_Filename { get; set; }
            public string Mes_Folderpath { get; set; }
        }
        public class Order
        {
            public bool CH1HighLevel { get; set; }
            public bool CH2HighLevel { get; set; }
            public bool CH1UpDownChange { get; set; }
            public bool CH2UpDownChange { get; set; }
            public bool CH1IGN { get; set; }
            public bool CH2IGN { get; set; }
            public bool CH1UP { get; set; }
            public bool CH1DOWN { get; set; }
            public bool CH1FWD { get; set; }
            public bool CH1RWD { get; set; }
            public bool CH1UPLeak { get; set; }
            public bool CH1DOWNLeak { get; set; }
            public bool CH1FWDLeak { get; set; }
            public bool CH1Pump { get; set; }
            public bool CH1LIN { get; set; }
            public bool CH1QuiescentCurrnt { get; set; }
            public string CH1UPindex { get; set; }
            public string CH1DOWNindex { get; set; }
            public string CH1FWDindex { get; set; }
            public string CH1RWDindex { get; set; }
            public string CH1UPLeakindex { get; set; }
            public string CH1DOWNLeakindex { get; set; }
            public string CH1FWDLeakindex { get; set; }
            public string CH1QuiescentCurrntIndex { get; set; }
            public bool CH2UP { get; set; }
            public bool CH2DOWN { get; set; }
            public bool CH2FWD { get; set; }
            public bool CH2RWD { get; set; }
            public bool CH2UPLeak { get; set; }
            public bool CH2DOWNLeak { get; set; }
            public bool CH2FWDLeak { get; set; }
            public bool CH2Pump { get; set; }
            public bool CH2LIN { get; set; }
            public string CH2UPindex { get; set; }
            public string CH2DOWNindex { get; set; }
            public string CH2FWDindex { get; set; }
            public string CH2RWDindex { get; set; }
            public string CH2UPLeakindex { get; set; }
            public string CH2DOWNLeakindex { get; set; }
            public string CH2FWDLeakindex { get; set; }

            public bool CH2QuiescentCurrnt { get; set; }
            public string CH2QuiescentCurrntIndex { get; set; }

            //AD
            public bool ADUP { get; set; }
            public bool ADDOWN { get; set; }
            public bool ADFWD { get; set; }
            public bool ADRWD { get; set; }
            public bool ADUPLeak { get; set; }
            public bool ADDOWNLeak { get; set; }
            public bool ADFWDLeak { get; set; }
            public bool ADQuiescentCurrnt { get; set; }
            public string ADUPindex { get; set; }
            public string ADDOWNindex { get; set; }
            public string ADFWDindex { get; set; }
            public string ADRWDindex { get; set; }
            public string ADUPLeakindex { get; set; }
            public string ADDOWNLeakindex { get; set; }
            public string ADFWDLeakindex { get; set; }
            public string ADQuiescentCurrntIndex { get; set; }

            //BE
            public bool BEUP { get; set; }
            public bool BEDOWN { get; set; }
            public bool BEFWD { get; set; }
            public bool BERWD { get; set; }
            public bool BEUPLeak { get; set; }
            public bool BEDOWNLeak { get; set; }
            public bool BEFWDLeak { get; set; }
            public bool BEQuiescentCurrnt { get; set; }
            public string BEUPindex { get; set; }
            public string BEDOWNindex { get; set; }
            public string BEFWDindex { get; set; }
            public string BERWDindex { get; set; }
            public string BEUPLeakindex { get; set; }
            public string BEDOWNLeakindex { get; set; }
            public string BEFWDLeakindex { get; set; }
            public string BEQuiescentCurrntIndex { get; set; }

            //CF
            public bool CFUP { get; set; }
            public bool CFDOWN { get; set; }
            public bool CFFWD { get; set; }
            public bool CFRWD { get; set; }
            public bool CFUPLeak { get; set; }
            public bool CFDOWNLeak { get; set; }
            public bool CFFWDLeak { get; set; }
            public bool CFQuiescentCurrnt { get; set; }
            public string CFUPindex { get; set; }
            public string CFDOWNindex { get; set; }
            public string CFFWDindex { get; set; }
            public string CFRWDindex { get; set; }
            public string CFUPLeakindex { get; set; }
            public string CFDOWNLeakindex { get; set; }
            public string CFFWDLeakindex { get; set; }
            public string CFQuiescentCurrntIndex { get; set; }

        }
        public class ProductCount
        {
            public int CH1Product { get; set; }
            public int CH1PassNum { get; set; }
            public int CH1FailNum { get; set; }
            public int CH2Product { get; set; }
            public int CH2PassNum { get; set; }
            public int CH2FailNum { get; set; }
        }
        public class LinConfig
        {
            //public string CH1LinBaudrate { get; set; }
            //public string CH2LinBaudrate { get; set; }
            public string LDFFileName { get; set; }
            public string UPSignalName { get; set; }
            public string DOWNSignalName { get; set; }
            public string FWDSignalName { get; set; }
            public string RWDSignalName { get; set; }
            public string Schedule_tables { get; set; }
            public string PowerSignalName { get; set; }
            public double PowerSignalValue { get; set; }

            //AD
            public string ADLDFFileName { get; set; }
            public string ADUPSignalName { get; set; }
            public string ADDOWNSignalName { get; set; }
            public string ADFWDSignalName { get; set; }
            public string ADRWDSignalName { get; set; }
            public string ADSchedule_tables { get; set; }
            public string ADPowerSignalName { get; set; }
            public double ADPowerSignalValue { get; set; }

            //BE
            public string BELDFFileName { get; set; }
            public string BEUPSignalName { get; set; }
            public string BEDOWNSignalName { get; set; }
            public string BEFWDSignalName { get; set; }
            public string BERWDSignalName { get; set; }
            public string BESchedule_tables { get; set; }
            public string BEPowerSignalName { get; set; }
            public double BEPowerSignalValue { get; set; }
            //CF
            public string CFLDFFileName { get; set; }
            public string CFUPSignalName { get; set; }
            public string CFDOWNSignalName { get; set; }
            public string CFFWDSignalName { get; set; }
            public string CFRWDSignalName { get; set; }
            public string CFSchedule_tables { get; set; }
            public string CFPowerSignalName { get; set; }
            public double CFPowerSignalValue { get; set; }
        }
        public class PLCPress
        {
            public string CH1Pressure { get; set; }
            public string CH2Pressure { get; set; }
            public string CH3Pressure { get; set; }
            public string CH4Pressure { get; set; }
            public string CH1Vol { get; set; }
            public string CH2Vol { get; set; }
            public string CH1Elect { get; set; }
            public string CH2Elect { get; set; }
            public bool ChkPLCPress { get; set; }
            public string CKCH1Vol { get; set; }
            public string CKCH2Vol { get; set; }
            public string CKCH1Current { get; set; }
            public string CKCH2Current { get; set; }
        }
    }
}
