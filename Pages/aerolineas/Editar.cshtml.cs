using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using MediatR;
using aspnetdemo2.domain.aerolineas;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;

namespace aspnetdemo2.Pages.aerolineas
{
    public class EditarModel : PageModel
    {

        
        [BindProperty]
        public AerolineaAEditar Detalle {get; set;}
        private readonly ILogger<EditarModel> _logger;
        private readonly IMediator mediator;
        private readonly IConfiguration configuracion;

        //private IEstudiantesRespository repo;


        public EditarModel(ILogger<EditarModel> logger,
         IMediator mediat,
         IConfiguration config)
        {
            _logger = logger;
            
            mediator = mediat;
            configuracion = config;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            var aerolinea = await mediator.Send(new LeerAerolineaPorId(id));
          
            if(aerolinea == null){
                return NotFound();
            }
            Detalle = new AerolineaAEditar() {
                Id = aerolinea.Id,
                Nombre = aerolinea.Nombre,
                PaisOrigen = aerolinea.PaisOrigen,
                Calificacion = aerolinea.Calificacion
            };

            return Page();
        }

        public async Task<IActionResult> OnPost(EditarAerolineaCommand cmd){

            if(!ModelState.IsValid){
                Detalle = new AerolineaAEditar() {
                Id = cmd.Id,
                Nombre = cmd.Nombre,
                PaisOrigen = cmd.PaisOrigen,
                Calificacion = cmd.Calificacion
                };
                return Page();
            }

            var aerolinea = await mediator.Send(new LeerAerolineaPorId(cmd.Id));
          
            if(aerolinea == null){
                return NotFound();
            }
            
            var res = await mediator.Send(cmd);

           

        
            return RedirectToPage("./Index");
        }

        public class AerolineaAEditar {
            public string Id { get; set; }  
             public string Nombre { get; set; }
            public string PaisOrigen { get; set; }
            public int Calificacion { get; set; }
            
        }
    }
}
