using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using Dwp.Adep.Ucb.DataServices.Models;
using Dwp.Adep.Ucb.DataServices;
namespace Dwp.Adep.Ucb.WebServices.Business_Logic
{

   
        /// <summary>
        /// Responsible for all behaviour associated with Agreement Year
        /// </summary>

    public sealed class AttachmentComponent
    {
        #region Constructors

        /// <summary>
        /// Private constructor to prevent instancing of this class
        /// </summary>
        private AttachmentComponent()
        { }

        #endregion

    
        #region UploadAttachment

        public static bool UploadAttachment(Attachment documentDetails, Stream documentStream, IRepository<Attachment> attachmentRepository, IRepository<AttachmentData> attachmentDataRepository)
        {
            //this implementation places the uploaded file in a Database BLOB
            //string filePath = "C:\\UploadedDocuments" + "\\" + documentUniqueIdentifier + "\\" + name;
            try
            {

                //FileStream outstream = File.Open(filePath, FileMode.Create, FileAccess.Write);
                MemoryStream outstream1 = new MemoryStream();
                //read from the input stream in 4K chunks
                //and save to output stream
                const int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                while ((count = documentStream.Read(buffer, 0, bufferLen)) > 0)
                {
                    outstream1.Write(buffer, 0, count);
                }
                byte[] DocumentImage = outstream1.ToArray();
                outstream1.Close();
                documentStream.Close();

                documentDetails.Code = Guid.NewGuid();

                AttachmentData data = new AttachmentData();
                data.AttachmentCode = documentDetails.Code;
                data.Code = Guid.NewGuid();
                data.DocumentBitmap = DocumentImage;
                
                attachmentRepository.Add(documentDetails);
                attachmentDataRepository.Add(data);
                return false;
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region DownloadAttachment

        public static Stream DownloadAttachment(Guid attachmentCode,IRepository<Attachment> dataRepository)
        {
            //open the file, this could throw an exception 
            //(e.g. if the file is not found)
            //having includeExceptionDetailInFaults="True" in config 
            // would cause this exception to be returned to the client
            try
            {

                Attachment RequiredDocument = dataRepository.Single(x => x.Code == attachmentCode,"AttachmentData");
                return new MemoryStream(RequiredDocument.AttachmentData.First().DocumentBitmap);
            }
            catch (IOException ex)
            {
                throw ex;
            }
            
        }

        #endregion

        #region GetAttachmentDetails

        /// <summary>
        /// Get the Agreement details for the document id specified.
        /// Call transactional overload
        /// </summary>
        /// <param name="userID">User's userID</param>
        /// <param name="documentUniqueIdentifier">The Document UniqueIdentifier</param>
        /// <returns>Agreement document details</returns>
        public static Attachment GetAttachmentDetails(Guid attachmentCode,IRepository<Attachment> dataRepository)
        {
            try
            {

                Attachment RequiredDocument = dataRepository.Single(x => x.Code == attachmentCode);
                return RequiredDocument;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            
        }

      

        #endregion

      
    }
    

}