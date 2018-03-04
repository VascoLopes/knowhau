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
    public class AdminController : ApiController
    {
        public IEnumerable<ADMIN> Get()
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.ADMINs.ToList();
            }
        }
        

        public ADMIN Get(String email)
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.ADMINs.FirstOrDefault(e => e.email == email);
            }
        }

        public ADMIN GetByName(String name)
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.ADMINs.FirstOrDefault(e => e.username == name);
            }
        }

        public HttpResponseMessage Put(String email, [FromBody]ADMIN ADMIN)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    var entity = entities.ADMINs.FirstOrDefault(e => e.email == email);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Admin with email " + email.ToString() + " not found to update");
                    }
                    else
                    {

                      
                        entity.email = ADMIN.email;
                        entity.name = ADMIN.name;
                        entity.password = ADMIN.password;
                        entity.username = ADMIN.username;
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


        public bool Get(String name, String passwd)
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                if (entities.ADMINs.FirstOrDefault(e => e.email == name) != null)
                {
                    if (entities.ADMINs.FirstOrDefault(e => e.email == name).email == name && entities.ADMINs.FirstOrDefault(e => e.email == name).password == passwd)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else if (entities.ADMINs.FirstOrDefault(e => e.username == name) != null)
                {
                    if (entities.ADMINs.FirstOrDefault(e => e.username == name).username == name && entities.ADMINs.FirstOrDefault(e => e.username == name).password == passwd)
                    {
                        return true;
                    }
                    else
                        return false;
                }

                return false;

            }

        }


        public HttpResponseMessage Post([FromBody] ADMIN admin)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    entities.ADMINs.Add(admin);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, admin);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        admin.name.ToString());

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
                    var entity = entities.ADMINs.FirstOrDefault(e => e.email == email);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "email = " + email.ToString() + " not found to delete");
                    }
                    else
                    {

                        var entity2 = entities.BAs.FirstOrDefault(e => e.adminemail== entity.email);
                        while (entity2 != null)
                        {
                            entities.BAs.Remove(entity2);
                            entities.SaveChanges();
                            entity2 = entities.BAs.FirstOrDefault(e => e.adminemail == entity.email);
                        }
                        entities.ADMINs.Remove(entity);
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
