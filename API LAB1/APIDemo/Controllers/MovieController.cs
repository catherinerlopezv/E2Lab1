using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDemo.Data;
using APIDemo.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIDemo
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        // GET: api/<Pelicular>
        [HttpGet]
       // public IEnumerable<Pelicula> Get([FromQuery] string key)
       // {
           // if (BaseCurrency != null && BaseCurrency.ToLower() == "usd")
             //   return Singleton.Instance.Peliculas.get(key);
            // en esta parte necesito que este ienumerable de nombre lista guarde los datos que se deben ingresar 
            /
           // IEnuerable<Pelicula> Lista = IEnumerable<Pelicula>();
           // IEnumerable<Pelicula> Lista;m
           // if (key != null)
              
               
               // return Singleton.Instance.Peliculas.get(key);

           //return Singleton.Instance.Peliculas;
        //}

        // GET api/<ExchangeRateController>/2020-08-01
        [HttpGet("{date}")]
        public ActionResult GetByDate([FromRoute] string date)
        {
            var result = Singleton.Instance.Peliculas.Where(x => x.Date == Convert.ToDateTime(date)).FirstOrDefault<Pelicula>();
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST api/<ExchangeRateController>
        [HttpPost]
        public ActionResult Post([FromBody] Pelicula newValue)
        {
            try
            {
                var result = Singleton.Instance.Peliculas.Where(x => x.Date == newValue.Date).FirstOrDefault<Pelicula>();
                if (result != null) return BadRequest();

                newValue.Id = Singleton.Instance.LastId + 1;
                Singleton.Instance.ExchangeRates.Add(newValue);
                Singleton.Instance.LastId++;
                return Created("",newValue);
            }
            catch (Exception ex)
            {
                return BadRequest(); 
            }
            
        }

        // PUT api/<ExchangeRateController>/2020-08-01
        [HttpPut("{date}")]
        public ActionResult Put(string date, [FromBody] Pelicula value)
        {
            var result = Singleton.Instance.Peliculas.Where(x => x.Date == Convert.ToDateTime(date)).FirstOrDefault< Pelicula>();
            if (result == null) return NotFound();
            value.Id = result.Id;
            Singleton.Instance.Peliculas.RemoveAll(x => x.Date == Convert.ToDateTime(date));
            Singleton.Instance.Peliculas.Add(value);
            return NoContent();
        }

        // DELETE api/<ExchangeRateController>/5
        [HttpDelete("{date}")]
        public ActionResult Delete(string date)
        {
            var result = Singleton.Instance.Peliculas.Where(x => x.Date == Convert.ToDateTime(date)).FirstOrDefault<Pelicula>();
            if (result == null) return NotFound();

            Singleton.Instance.Peliculas.RemoveAll(x => x.Date == Convert.ToDateTime(date));
            return NoContent();
        }
    }
}
