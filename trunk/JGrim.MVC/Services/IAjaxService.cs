using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace JGrim.MVC.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAjaxService" in both code and config file together.
    [ServiceContract]
    public interface IAjaxService
    {
        [OperationContract]
        int LoadImagesToDB();


    }
}
