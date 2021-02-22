using Skybrud.Essentials.Http;

namespace Skybrud.Integrations.AffaldPlus.Responses {

    public class AffaldPlusResponse : HttpResponseBase {

        #region Constructors

        protected AffaldPlusResponse(IHttpResponse response) : base(response) { }

        #endregion

        #region Static methods

        public static void ValidateResponse(IHttpResponse response) { }

        #endregion
        
    }

    public class AffaldPlusResponse<T> : AffaldPlusResponse {

        #region Properties

        public T Body { get; protected set; }

        #endregion

        #region Constructors

        protected AffaldPlusResponse(IHttpResponse response) : base(response) { }

        #endregion

    }

}