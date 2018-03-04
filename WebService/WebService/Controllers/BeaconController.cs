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
    public class BeaconController : ApiController
    {

        public IEnumerable<BEACON> Get()
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.BEACONs.ToList();
            }
        }

        public BEACON Get(String id)
        {
            using (knowhauEntities entities = new knowhauEntities())
            {
                return entities.BEACONs.FirstOrDefault(e => e.beaconID == id);
            }
        }

        public HttpResponseMessage Post([FromBody] BEACON beacon, String admin, String mensagem, int super)
        {
            try
            {
                HttpConfiguration config = GlobalConfiguration.Configuration;

                config.Formatters.JsonFormatter
                            .SerializerSettings
                            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                using (knowhauEntities entities = new knowhauEntities())
                {
                    LOGWEBAPP web = new LOGWEBAPP();
                    web.date = DateTime.Now;
                    web.eventtype="I";
                    web.username =admin;
                    if (super == 0)
                    {
                        BA id = new BA();
                        id.beaconID = beacon.beaconID;
                        id.adminemail = admin;
                        entities.BAs.Add(id);
                    }
                    
                    CONTENT cont = new CONTENT();
                    cont.beaconID = beacon.beaconID;
                    cont.contentmsg = mensagem;
                    entities.BEACONs.Add(beacon);
                    
                    entities.CONTENTs.Add(cont);
                    entities.LOGWEBAPPs.Add(web);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, beacon);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        beacon.name.ToString());


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

        public HttpResponseMessage Put(String beaconID, [FromBody]BEACON BEACON, String admin)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    BEACON entity = new BEACON();
                    entity = entities.BEACONs.FirstOrDefault(e => e.beaconID == beaconID);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Beacon with id " + beaconID.ToString() + " not found to update");
                    }
                    else
                    {

                        entity.beaconID = BEACON.beaconID;
                        entity.majorvalue = BEACON.majorvalue;
                        entity.minorvalue = BEACON.minorvalue;
                        entity.model = BEACON.model;
                        entity.name = BEACON.name;
                        entities.SaveChanges();
                        LOGWEBAPP web = new LOGWEBAPP();
                        web.date = DateTime.Now;
                        web.eventtype = "U";
                        web.username = admin;
                        entities.LOGWEBAPPs.Add(web);
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

        public HttpResponseMessage Put(String beaconID, [FromBody]BEACON BEACON)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    BEACON entity = new BEACON();
                    entity = entities.BEACONs.FirstOrDefault(e => e.beaconID == beaconID);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Beacon with id " + beaconID.ToString() + " not found to update");
                    }
                    else
                    {

                        entity.beaconID = BEACON.beaconID;
                        entity.majorvalue = BEACON.majorvalue;
                        entity.minorvalue = BEACON.minorvalue;
                        entity.model = BEACON.model;
                        entity.name = BEACON.name;
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

        public HttpResponseMessage Delete(String beaconID, string admin)
        {
            try
            {
                using (knowhauEntities entities = new knowhauEntities())
                {
                    var entity = entities.BEACONs.FirstOrDefault(e => e.beaconID == beaconID);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "beaconID = " + beaconID.ToString() + " not found to delete");
                    }
                    else
                    {
                        var entity3 = entities.CONTENTs.FirstOrDefault(e => e.beaconID == entity.beaconID);
                        var entity4 = entities.HISTORICs.FirstOrDefault(e => e.contentID == entity3.contentID);
                        while (entity4 != null)
                        {
                            entities.HISTORICs.Remove(entity4);
                            entities.SaveChanges();
                            entity4 = entities.HISTORICs.FirstOrDefault(e => e.contentID == entity3.contentID);
                        }
                        
                        while (entity3 != null)
                        {
                            entities.CONTENTs.Remove(entity3);
                            entities.SaveChanges();
                            entity3 = entities.CONTENTs.FirstOrDefault(e => e.beaconID == entity.beaconID);
                        }
                        
                        var entity2 = entities.BAs.FirstOrDefault(e => e.beaconID == entity.beaconID);
                        while (entity2 != null)
                        {
                            entities.BAs.Remove(entity2);
                            entities.SaveChanges();
                            entity2 = entities.BAs.FirstOrDefault(e => e.beaconID == entity.beaconID);
                        }
                        entities.BEACONs.Remove(entity);
                        entities.SaveChanges();
                        BA id = new BA();
                        //id=entities.BAs.FirstOrDefault(e => e.beaconID == beaconID);
                        LOGWEBAPP web = new LOGWEBAPP();
                        web.date = DateTime.Now;
                        web.eventtype = "D";
                        web.username = admin;
                        entities.LOGWEBAPPs.Add(web);
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
