
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace BenQGuru.eMES.Web.Helper
{
    public class AcceptAllPolicy : System.Net.ICertificatePolicy
    {

        /// <summary>
        /// construct method
        /// </summary>
        public AcceptAllPolicy()
        {
        }

        /// <summary>
        ///  check if request is Validate
        /// </summary>
        /// <param name="srvPoint">ServicePoint</param>
        /// <param name="certificate">X509Certificate</param>
        /// <param name="request">WebRequest</param>
        /// <param name="certificateProblem">int certificateProblem</param>
        /// <returns>true</returns>
        public bool CheckValidationResult(
            ServicePoint srvPoint,
            X509Certificate certificate,
            WebRequest request,
            int certificateProblem
            )
        {
            return true;
        }
    }
}
