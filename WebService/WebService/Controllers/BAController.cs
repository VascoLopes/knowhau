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
    public class BAController : ApiController
    {
        public IEnumerable<BA> Get()
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.BAs.ToList();
            }
        }

        public HttpResponseMessage Post([FromBody] BA ba)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    entities.BAs.Add(ba);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, ba);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        ba.baID.ToString());

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

        public HttpResponseMessage Put(String beaconID, [FromBody]BA BA)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    var entity = entities.BAs.FirstOrDefault(e => e.beaconID == beaconID);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Beacon with id " + beaconID.ToString() + " not found to update");
                    }
                    else
                    {


                        entity.beaconID = BA.beaconID;
                        entity.adminemail = BA.adminemail;
                        entity.baID = BA.baID;
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        public HttpResponseMessage Delete(int baID)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    var entity = entities.BAs.FirstOrDefault(e => e.baID == baID);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "baID = " + baID.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.BAs.Remove(entity);
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
