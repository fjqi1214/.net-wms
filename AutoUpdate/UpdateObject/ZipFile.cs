using System;
using System.IO;

using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;


namespace Tools
{
	/// <summary>
	/// clsZipFile 的摘要说明。
	/// </summary>
	public class ZipFile
	{
		private static string TemporaryDirectory = System.Environment.CurrentDirectory + @"\TMP";

		public ZipFile()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		//解压缩文件
		public static void UnZipFile(string file)
		{
			ZipInputStream s = new ZipInputStream(File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "\\" + file));
		
			ZipEntry theEntry;
			try
			{
				//循环获取压缩实体
				while ((theEntry = s.GetNextEntry()) != null) 
				{
					//string directoryName = Path.GetDirectoryName(theEntry.Name);
					string fileName      = Path.GetFileName(theEntry.Name);
			
					if (fileName != String.Empty 
						&& fileName.IndexOf("AutoUpdate") < 0
						&& fileName.IndexOf("SharpZipLib") < 0) 
					{
						if(File.Exists(fileName))
						{
							try
							{
								File.Delete(fileName);
							}
							catch
							{}
						}

						FileStream streamWriter = File.Create(fileName);
				
						try
						{
							int size = 2048;
							byte[] data = new byte[2048];
							while (true) 
							{
								size = s.Read(data, 0, data.Length);
								if (size > 0) 
								{
									//写文件
									streamWriter.Write(data, 0, size);
								} 
								else 
								{
									break;
								}
							}
						}
						catch
						{
						}
						finally
						{
							streamWriter.Close();
						}
					}
					
//					//进度条累加
//					if(pgbDownload.Value < 100)
//					{
//						pgbDownload.Value += 1;
//
//
//						pgbDownload.Refresh();
//					}

					System.Threading.Thread.Sleep(100);
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				s.Close();
			}
		}
		/// <summary>
		/// 压缩指定目录内的文件
		/// </summary>
		/// <param name="pathName">目录路径</param>
		/// <param name="comparedFileName">压缩后的压缩文件名</param>
		/// <param name="compsLevel">压缩水平</param>
		public static void ZipFiles(string pathName,string comparedFileName,int compsLevel)
		{
            string[] dirNames = Directory.GetDirectories(pathName);
			string[] fileNames = Directory.GetFiles(pathName);
		    
			Crc32 crc = new Crc32();

			if(File.Exists(comparedFileName))
			{
				File.Delete(comparedFileName);
			}
			if(Directory.Exists(TemporaryDirectory))
			{
				Directory.Delete(TemporaryDirectory,true);
			}
			
			Directory.CreateDirectory(TemporaryDirectory);
			

			ZipOutputStream s = new ZipOutputStream(File.Create(comparedFileName));

			try
            {
                foreach (string dir in dirNames)
                {
                    AddZipEntry(pathName, dir, s, comparedFileName, compsLevel, crc, out s);
                }

                foreach (string file in fileNames)
                {
                    if (file.IndexOf(comparedFileName) >= 0)
                    {
                        continue;
                    }
                    AddZipEntry(pathName, file, s, comparedFileName, compsLevel, crc, out s);
                }

				if(Directory.Exists(TemporaryDirectory))
				{
					Directory.Delete(TemporaryDirectory,true);
				}

			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				s.Finish();
				s.Close();
			}
		}

        private static void AddZipEntry(string pathName, string fileDir, ZipOutputStream zipS, string comparedFileName, int compsLevel, Crc32 crc, 
            out ZipOutputStream zipRtn)
        {
            if (Directory.Exists(fileDir)) //文件夹的处理
            {
                DirectoryInfo di = new DirectoryInfo(fileDir);
                //没有子目录
                if(di.GetDirectories().Length <= 0)   
                {
                    ZipEntry z = new ZipEntry(ShortDir(pathName,fileDir) + "/"); 
                    zipS.PutNextEntry(z);
                }
                //获取子目录
                foreach(DirectoryInfo tem in di.GetDirectories()) 
                {
                    ZipEntry z = new ZipEntry(ShortDir(pathName,tem.FullName) + "/");
                    zipS.PutNextEntry(z);
                    AddZipEntry(pathName, tem.FullName, zipS, comparedFileName, compsLevel, crc, out zipS); 
                }
                //获取此目录的文件
                foreach(FileInfo temp in di.GetFiles())  
                {
                    AddZipEntry(pathName, temp.FullName, zipS, comparedFileName, compsLevel, crc, out zipS); 
                }        
           }
           else if (File.Exists(fileDir)) //文件的处理
           {
               zipS.SetLevel(compsLevel); // 0 - store only to 9 - means best compression
               
               string fileName = fileDir.Remove(0, fileDir.LastIndexOf(@"\") + 1);
               fileName = TemporaryDirectory + @"\" + fileName;


               File.SetAttributes(fileDir, FileAttributes.Normal);

               File.Copy(fileDir,
                   fileName, true);

               FileStream fs = File.OpenRead(fileName);

               byte[] buffer = new byte[fs.Length];
               fs.Read(buffer, 0, buffer.Length);
               ZipEntry entry = new ZipEntry(ShortDir(pathName,fileDir));

               entry.DateTime = (new FileInfo(fileDir)).LastWriteTime;

               // set Size and the crc, because the information
               // about the size and crc should be stored in the header
               // if it is not set it is automatically written in the footer.
               // (in this case size == crc == -1 in the header)
               // Some ZIP programs have problems with zip files that don't store
               // the size and crc in the header.
               entry.Size = fs.Length;
               fs.Close();

               crc.Reset();
               crc.Update(buffer);

               entry.Crc = crc.Value;

               zipS.PutNextEntry(entry);

               zipS.Write(buffer, 0, buffer.Length);

               File.Delete(fileName);

           }

           zipRtn = zipS;
        }

        private static string ShortDir(string pathName,string s)
        {
            //将文件的绝对路径转为相对路径
            if (pathName.Substring(pathName.Length - 1, 1) != @"\")
            {
                pathName = pathName + "\\";
            }
            string d = s.Replace(pathName, "");
            return d;
        }

	}
}
