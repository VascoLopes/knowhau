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
    public class SuperAdminController : ApiController
    {
        public IEnumerable<SUPERADMIN> Get()
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.SUPERADMINs.ToList();
            }
        }



        public SUPERADMIN Get(String email)
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.SUPERADMINs.FirstOrDefault(e => e.email == email);
            }
        }

        public SUPERADMIN GetByName(String username)
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.SUPERADMINs.FirstOrDefault(e => e.username == username);
            }
        }



        public bool Get(String name, String passwd)
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                if (entities.SUPERADMINs.FirstOrDefault(e => e.email == name) != null)
                {
                    if (entities.SUPERADMINs.FirstOrDefault(e => e.email == name).email == name && entities.SUPERADMINs.FirstOrDefault(e => e.email == name).password == passwd)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else if (entities.SUPERADMINs.FirstOrDefault(e => e.username == name) != null)
                {
                    if (entities.SUPERADMINs.FirstOrDefault(e => e.username == name).username == name && entities.SUPERADMINs.FirstOrDefault(e => e.username == name).password == passwd)
                    {
                        return true;
                    }
                    else
                        return false;
                }

                return false;

            }

        }




        public HttpResponseMessage Put(String email, [FromBody]SUPERADMIN SUPERADMIN)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    var entity = entities.SUPERADMINs.FirstOrDefault(e => e.email == email);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "SUPERADMIN with email " + email.ToString() + " not found to update");
                    }
                    else
                    {


                        entity.email = SUPERADMIN.email;
                        entity.name = SUPERADMIN.name;
                        entity.password = SUPERADMIN.password;
                        entity.username = SUPERADMIN.username;
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



        public HttpResponseMessage Post([FromBody] SUPERADMIN superadmin)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    entities.SUPERADMINs.Add(superadmin);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, superadmin);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        superadmin.name.ToString());

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

        public HttpResponseMessage Delete(String email)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    var entity = entities.SUPERADMINs.FirstOrDefault(e => e.email == email);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "SuperAdmin with email = " + email.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.SUPERADMINs.Remove(entity);
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

