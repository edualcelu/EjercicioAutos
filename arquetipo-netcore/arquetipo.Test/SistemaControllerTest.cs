using arquetipo.API.Controllers;
using arquetipo.Domain.Interfaces;
using arquetipo.Entity.Dto;
using arquetipo.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace arquetipo.Test 
{
    [TestClass]
public class SistemaControllerTest : BaseTest
    {
        private readonly ICliente servicio;
        [TestMethod]
        public async Task ObtenerCliente()
        {
            //prepacion
            string nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            contexto.Clientes.Add(new Cliente()
            {

                ClIdentificacion = "1720477332",
                ClNombres = "Eduardo",
                ClEdad = 17,
                ClFechaNacimiento = DateTime.Now,
                ClApellidos = "Araujo",
                ClDireccion ="San ANtonio",
                ClTelefono="0995691214",
                ClEstadoCivil = "Soltero",
                ClIdentificacionConyuge="1720477346",
                ClNombreConyuge="Cinthia",
                ClSujetoCredito=true
            });
            contexto.Clientes.Add(new Cliente()
            {
                ClIdentificacion = "1720477334",
                ClNombres = "Eduardo",
                ClEdad = 17,
                ClFechaNacimiento = DateTime.Now,
                ClApellidos = "Araujo",
                ClDireccion = "San ANtonio",
                ClTelefono = "0995691214",
                ClEstadoCivil = "Soltero",
                ClIdentificacionConyuge = "1720477346",
                ClNombreConyuge = "Cinthia",
                ClSujetoCredito = true
            });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);

            //Prueba
            //var controlador = new ClienteImplementacion(contexto2);
            var controlador = new ClienteController(servicio);
            var respuesta = await controlador.GetClientes();
            //verificacion
            // var cliente = new List<Cliente>(respuesta.Resultado);

            List<Cliente> cliente = JsonConvert.DeserializeObject<List<Cliente>>(respuesta.Resultado.ToString());
            Assert.AreEqual(2, cliente.Count);

        }
        


    }
}
