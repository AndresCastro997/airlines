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
using Microsoft.AspNetCore.Authorization;

namespace aspnetdemo2.Pages.aerolineas
{
    [Authorize(Roles = "Admin")]
    public class BorrarModel : PageModel
    {

        
        [BindProperty]
        public AerolineaABorrar Detalle {get; set;}
        private readonly ILogger<BorrarModel> _logger;
        private readonly IMediator mediator;

        public BorrarModel(ILogger<BorrarModel> logger,
         IMediator mediat)
        {
            _logger = logger;
            mediator = mediat;
            
        }

        public async Task<IActionResult> OnGet(string id)
        {
           var aerolinea = await mediator.Send(new LeerAerolineaPorId(id));
          
            if(aerolinea == null){
                return NotFound();
            }
            Detalle = new AerolineaABorrar() {
                Id = aerolinea.Id,
                Nombre = aerolinea.Nombre,
            };

            return Page();
        }

        public async Task<IActionResult> OnPost(BorrarAerolineaCommand cmd){

            if(!ModelState.IsValid){
                Detalle = new AerolineaABorrar() {
                Id = cmd.Id,
               
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

        public class AerolineaABorrar {
            public string Id { get; set; }  
             public string Nombre { get; set; }
            
            
        }
    }
}
