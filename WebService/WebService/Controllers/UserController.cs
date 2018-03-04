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
    public class UserController : ApiController
    {
        public IEnumerable<USERM> Get()
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.USERMs.ToList();
            }
        }

        public USERM Get(String name)
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.USERMs.FirstOrDefault(e => e.username == name);
            }
        }

        /*  public HttpResponseMessage Post(USERM utilizador)
          {
              HttpResponseMessage result;
              if (ModelState.IsValid)
              {
                  using (knowhauEntities db = new knowhauEntities())
                  {
                      db.USERMs.Add(utilizador);
                      db.SaveChanges();
                  }
                  result = Request.CreateResponse(HttpStatusCode.Created, utilizador);
              }
              else
              {
                  result = Request.CreateResponse(HttpStatusCode.BadRequest, "Server failed to save data");
              }
              return result;
          }*/
        public HttpResponseMessage Put(String email, [FromBody]USERM utilizador)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    var entity = entities.USERMs.FirstOrDefault(e => e.email == email);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Utilizador with email " + email.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.birthdate = utilizador.birthdate;
                        entity.email = utilizador.email;
                        entity.genre = utilizador.genre;
                        entity.name = utilizador.name;
                        entity.password = utilizador.password;
                        entity.username = utilizador.username;

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
                if (entities.USERMs.FirstOrDefault(e => e.email == name) != null)
                {
                    if (entities.USERMs.FirstOrDefault(e => e.email == name).email == name && entities.USERMs.FirstOrDefault(e => e.email == name).password == passwd) 
                    {
                        return true;
                    }
                    else
                        return false;
                }else if (entities.USERMs.FirstOrDefault(e => e.username == name) != null)
                {
                    if (entities.USERMs.FirstOrDefault(e => e.username == name).username == name && entities.USERMs.FirstOrDefault(e => e.username == name).password == passwd)
                    {
                        return true;
                    }
                    else
                        return false;
                }

                return false;

            }

        }

        public HttpResponseMessage Post([FromBody] USERM utilizador)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    entities.USERMs.Add(utilizador);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, utilizador);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        utilizador.name.ToString());

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
                    var entity = entities.USERMs.FirstOrDefault(e => e.email == email);
                
                    
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User with email = " + email.ToString() + " not found to delete");
                    }
                    else
                    {
                        var entity2= entities.HISTORICs.FirstOrDefault(e => e.userMAIL == entity.email);
                        while(entity2 != null)
                        {
                            entities.HISTORICs.Remove(entity2);
                            entities.SaveChanges();
                            entity2 = entities.HISTORICs.FirstOrDefault(e => e.userMAIL == entity.email);
                        }
                        entities.USERMs.Remove(entity);
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
