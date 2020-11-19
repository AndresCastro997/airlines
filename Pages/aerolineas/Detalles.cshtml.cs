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

namespace aspnetdemo2.Pages.aerolineas
{
    public class DetallesModel : PageModel
    {

        
        public AerolineaDetalleModel Detalle {get; set;}
        private readonly ILogger<CrearModel> _logger;
        private IMediator mediator;

        public DetallesModel(ILogger<CrearModel> logger
        ,IMediator media)
        {
            _logger = logger;
            
            mediator = media;
        }

        public async Task<IActionResult> OnGet(string id)
        {
           var aerolinea = await  mediator.Send(new LeerAerolineaPorId(id));
            if(aerolinea == null){
                return NotFound();
            }
            Detalle = aerolinea;

            return Page();
        }

    }
}
