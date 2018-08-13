using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ServiceModel;
using Dwp.Adep.Ucb.ResourceLibrary;
using UcbWeb.UcbService;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace UcbWeb.Helpers
{
    /// <summary>
    /// Class to centrally manage exceptions
    /// </summary>
    public class ExceptionManager
    {

        /// <summary>
        /// Handle exception
        /// </summary>
        /// <param name="e">Exception to handle</param>
        /// <param name="serviceObject">Service communication object to abort if not already closed</param>
        /// <returns>String containing message about the error</returns>
        /// <remarks>Error message will be changed if the error is a data integrity or data concurrecny issue so that the user is warned appropriately</remarks>
        public static string HandleException(Exception e, ICommunicationObject serviceObject)
        {
            // Publish the exception information
            PublishException(e);

            // If service isn't already closed then abort communication
            if (null != serviceObject && serviceObject.State != CommunicationState.Closed)
            {
                // Abort the service
                serviceObject.Abort();
            }

            // Message to pass back to user
            string returnMessage = null;

            // Data integrity problem
            if (e is FaultException<DataIntegrityFault>)
            {
                // Set messaage
                returnMessage = Resources.MESSAGE_DATACANNOTDELETE;
            }
            // Data concurrency problem
			else if (e is FaultException<DataConcurrencyFault>)
            {
                // Set messaage
                returnMessage = Resources.MESSAGE_DATACONCURRENCYFAILURE;
            }
            // Data unique constraint problem
            else if (e is FaultException<UniqueConstraintFault>)
            {
                // Set message
                returnMessage = Resources.MESSAGE_UNIQUEKEYCONSTRAINT;
            }
            // General problem
            else
            {
                // Rethrow as this is an unexpected problem
                throw e;
            }

              return returnMessage;

        }

        /// <summary>
        /// Publish an exception 
        /// </summary>
        /// <param name="e"></param>
        public static void PublishException(Exception e)
        {
            bool rethrow = ExceptionPolicy.HandleException(e, "AdepExceptionPolicy");
        }

    }
}