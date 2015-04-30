using AutoMapper;
using Personal.Entities;
using Personal.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Personal.WebApi.Controllers
{
    public class JobsController : ApiController
    {
        private readonly IHrContext context;
        public JobsController(IHrContext ctx)
        {
            context = ctx;
        }
        // GET api/<controller>
    /*    public IEnumerable<Job> Get()
        {
            return context.Jobs;
        }*/

        public IHttpActionResult Get()
        {
            return Ok(context.Jobs);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string id)
        {
           // var job = context.Jobs.Where(x=>x.JobId.Equals(id));
            var job = context.Jobs.Find(id);
           // return job != null ? Ok(job) : (IHttpActionResult)NotFound();

            if (job != null)
            {
                return Ok(job);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<controller>
        public IHttpActionResult Post(Job job)
        {
          //  return context.Jobs.Add(job);
            var addedJob = context.Jobs.Add(job);
            //return RedirectToRoute("DefaultApi", new { addedJob.JobId });
            return CreatedAtRoute("DefaultApi",
                new { controller = "Jobs", 
                    id = addedJob.JobId }, 
                    addedJob);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(Job job)
        {

            Mapper.CreateMap<Job, Job>();
            var dbJob = context.Jobs.Find(job.JobId);
            if(dbJob != null)
            {
                Mapper.Map(job, dbJob);
             /*   dbJob.JobTitle = job.JobTitle;
                dbJob.MaxSalary = job.MaxSalary;
                dbJob.MinSalary = job.MinSalary;*/
            }
            return Ok(context.SaveChanges());
            //return new HttpResponseMessage(HttpStatusCode.OK).Headers.Location = 
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(string id)
        {
            var dbJob = context.Jobs.Find(id);
            if (dbJob != null)
            {
                return Ok(context.Jobs.Remove(dbJob));
            }
            return NotFound();
            
        }
    }
}