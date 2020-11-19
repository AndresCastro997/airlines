using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MongoDB.Driver;

namespace aspnetdemo2.domain.aerolineas {


    public class CrearAerolineaCommand : IRequest<bool> {
        public CrearAerolineaCommand(
                string nombre, 
                string paisOrigen, 
                int calificacion
                )
        {
            
            Nombre = nombre;
            PaisOrigen = paisOrigen;
            Calificacion = calificacion;
          
        }
        public CrearAerolineaCommand()
        {
            
        }

     
        public string Nombre { get; set; }
        public string PaisOrigen { get;set; }
        public int Calificacion { get; set; }
   
    }


    public class CrearAerolineaCommandValidator : AbstractValidator<CrearAerolineaCommand>
    {
        public CrearAerolineaCommandValidator()
        {
            
            RuleFor(m => m.Nombre).NotEmpty().MinimumLength(3);
            RuleFor(m => m.PaisOrigen).NotEmpty().MinimumLength(3);
            RuleFor(m => m.Calificacion).NotEmpty()
                        .Must( p => p >= 0);
            
                
        }
    }


    public class CrearAerolineaCommandHandler
           : IRequestHandler<CrearAerolineaCommand, bool>
    {
        private readonly IMongoCollection<Aerolinea> aerolineas;

        public CrearAerolineaCommandHandler(IEstudiantesDatabaseSettings settings)
        {
            var cliente = new MongoClient(settings.ConnectionString);
            var db = cliente.GetDatabase(settings.DatabaseName);

            aerolineas = db.GetCollection<Aerolinea>("Aerolineas");
        }

        public async Task<bool> Handle(CrearAerolineaCommand request, CancellationToken cancellationToken)
        {
           
           var mgAerolinea = new Aerolinea() {
                Nombre = request.Nombre,
                PaisOrigen = request.PaisOrigen,
                Calificacion = request.Calificacion
                
            };

            await aerolineas.InsertOneAsync(mgAerolinea);

            return true;
            
        }
    }

    

}