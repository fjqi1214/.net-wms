using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Security.Cryptography;    
using NUnit.Framework;



using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;   
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Domain.BaseSetting;


namespace UserNunit
{
	[TestFixture] 
	public class UserPasswordTest
	{
		private IDomainDataProvider _domainDataProvider;
		
		[SetUp]
		public void SetUp()
		{
			_domainDataProvider = DomainDataProviderManager.DomainDataProvider();
			
			if (_domainDataProvider.CustomSearch(typeof(User), new object[]{"UserCode"}) == null)
			{
				User user = new User();
				user.UserCode = "UserCode";
				user.UserPassword = "PP";
				user.UserDepartment = "GRD2";
				user.MaintainUser  = "UserCode";
				user.MaintainDate =20050412;
				user.MaintainTime = 190212;
				_domainDataProvider.Insert(user); 
			}

		}

		[Test]
		public void TestUserPassword()
		{
			MD5 md5 = new MD5CryptoServiceProvider();

			for (int i=0; i<50; i++)
			{
				string _password = Guid.NewGuid().ToString();
			
	         
				System.Console.WriteLine(_password);   
            
				User user = new User();
				user.UserCode = "UserCode";
				user.UserPassword = System.BitConverter.ToString(md5.ComputeHash(Encoding.ASCII.GetBytes(_password)));
				user.UserDepartment = "GRD2";
				user.MaintainUser  = "Admin";
				user.MaintainDate =20050412;
				user.MaintainTime = 190212;
				_domainDataProvider.Update(user); 

				user = (User)_domainDataProvider.CustomSearch(typeof(User), new object[]{"UserCode"}); 

				Assert.AreEqual(user.UserPassword, System.BitConverter.ToString(md5.ComputeHash(Encoding.ASCII.GetBytes(_password))));
			}
		}
	}
}
