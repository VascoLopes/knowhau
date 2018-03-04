using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using System.Data.Entity.Validation;
using System.Web.Http.Cors;

namespace WebService.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class LogWebbAppController : ApiController
    {
        public IEnumerable<LOGWEBAPP> Get()
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.LOGWEBAPPs.ToList();
            }
        }

        public HttpResponseMessage Post([FromBody] LOGWEBAPP logwebapp)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    entities.LOGWEBAPPs.Add(logwebapp);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, logwebapp);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        logwebapp.eventtype.ToString());

                    return message;
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int logwaID)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    var entity = entities.LOGWEBAPPs.FirstOrDefault(e => e.logwaID == logwaID);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "LogWebAppID = " + logwaID.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.LOGWEBAPPs.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
