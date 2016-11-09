﻿using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Murtain.OAuth2.Web.Configuration
{
    static class Certificate
    {
        public static X509Certificate2 Get()
        {
            var assembly = typeof(Certificate).Assembly;
            using (var stream = assembly.GetManifestResourceStream("Murtain.OAuth2.Web.Configuration.Murtian.OAuth2.Web.pfx"))
            {
                return new X509Certificate2(ReadStream(stream), "idsrv3test");
            }
        }

        private static byte[] ReadStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}