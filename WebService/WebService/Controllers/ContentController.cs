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
    public class ContentController : ApiController
    {


        public IEnumerable<CONTENT> Get()
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.CONTENTs.ToList();
            }
        }
        public CONTENT Get(int id)
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.CONTENTs.FirstOrDefault(e => e.contentID== id);
            }
        }

        public HttpResponseMessage Post([FromBody] CONTENT content)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    entities.CONTENTs.Add(content);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, content);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        content.contentmsg.ToString());

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
        public HttpResponseMessage Delete(int contentID)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    var entity = entities.CONTENTs.FirstOrDefault(e => e.contentID == contentID);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "contentID = " + contentID.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.CONTENTs.Remove(entity);
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

        public HttpResponseMessage Put(int contentID, [FromBody]CONTENT CONTENT)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    CONTENT entity = new CONTENT();
                    entity = entities.CONTENTs.FirstOrDefault(e => e.contentID == contentID);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Content with id " + contentID.ToString() + " not found to update");
                    }
                    else
                    {

                        entity.beaconID = CONTENT.beaconID;
                        entity.contentID = CONTENT.contentID;
                        entity.contentmsg = CONTENT.contentmsg;
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
    }
}
