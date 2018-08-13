using System;
namespace Dwp.Adep.Ucb.WebServices.MessageContracts.Exceptions
{
    public interface IExceptionManager
    {
        void PublishException(Exception e);
        void ShieldException(Exception e);
    }
}
