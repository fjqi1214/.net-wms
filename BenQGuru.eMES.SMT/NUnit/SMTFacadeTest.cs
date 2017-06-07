using System;

using NUnit.Framework;

using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.SMT ;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Common.PersistBroker;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.SMT.UnitTest
{
	/// <summary>
	/// SMTFacadeTest 的摘要说明。
	/// </summary>
	[TestFixture]
	public class SMTFacadeTest
	{
        private OLEDBPersistBroker persistBroker = null;
        
        
        public SMTFacadeTest()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


        [Test]
        public void TestSMTCopy()
        {
            persistBroker = new OLEDBPersistBroker("Provider=OraOLEDB.Oracle.1;Password=emes;Persist Security Info=True;User ID=emes;Data Source=sqcdemo");
            
            // clear data

            // insert info of MO UT_MOCODE1
            string sql = "INSERT INTO TBLSMTRESBOM (MOCODE,ROUTECODE,OPCODE,RESCODE,STATIONCODE,ITEMCODE,OPBOMCODE,OPBOMVER,LOTNO,PCBA,BIOS,VERSION,VENDERITEMCODE,VENDORCODE,DATECODE,FEEDERCODE,MUSER,MDATE,MTIME,EATTRIBUTE1,OBITEMCODE) VALUES ('UT_MOCODE1','UT_ROUTECODE1','UT_OPERATIONCODE1','UT_RESOURCE1','{0}','UT_ITEMCODE1','UT_ROUTECODE1','OPBOMVER1','LOTNO','PCBA','BIOS','VERSION','VENDERITEMCODE','VENDORCODE','DATECODE','FEEDER1','vizo UnitTest',20050509,0,NULL,'P007-10594-03')" ;

            persistBroker.Execute( string.Format(sql,"UT_STATION1") );
            persistBroker.Execute( string.Format(sql,"UT_STATION2") );
            persistBroker.Execute( string.Format(sql,"UT_STATION3") );
            


            // copy from UT_MOCODE1 to UT_MOCODE2
            BenQGuru.eMES.SMT.SMTFacade facade = new BenQGuru.eMES.SMT.SMTFacade() ;
            facade.CopySMTResourceBOM("UT_MOCODE1","UT_MOCODE2","UT_ROUTECODE1","UT_OPERATIONCODE1","UT_RESOURCE1") ;

            // check smtbom count of  UT_MOCODE2
            int cnt1 = facade.QuerySMTResourceBOMCount(string.Empty,"UT_MOCODE1",string.Empty ,string.Empty ,string.Empty ,string.Empty) ;
            int cnt2 = facade.QuerySMTResourceBOMCount(string.Empty,"UT_MOCODE2",string.Empty ,string.Empty ,string.Empty ,string.Empty) ;

            // clear data
            sql = "delete from tblsmtresbom where mocode like 'UT_%'" ;
            persistBroker.Execute( sql );

            Console.WriteLine("cnt1={0} , cnt2={1}",cnt1,cnt2) ;
            Assert.AreEqual(cnt1,cnt2) ;
            Assert.AreEqual(3,cnt1) ;



        }
	}
}
