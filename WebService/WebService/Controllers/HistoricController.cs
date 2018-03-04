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
    public class HistoricController : ApiController
    {

        public IEnumerable<HISTORIC> Get()
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.HISTORICs.ToList();
            }
        }

        public List<HISTORIC> Get(String user)
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.HISTORICs.Where(e => e.userMAIL == user).ToList();
            }
        }



        public HttpResponseMessage Post([FromBody] HISTORIC historic)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    entities.HISTORICs.Add(historic);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, historic);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        historic.historicID.ToString());

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

        public HttpResponseMessage Delete(int historicID)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    var entity = entities.HISTORICs.FirstOrDefault(e => e.historicID == historicID);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "historicID = " + historicID.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.HISTORICs.Remove(entity);
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
