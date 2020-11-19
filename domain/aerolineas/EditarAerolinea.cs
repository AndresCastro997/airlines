using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MongoDB.Driver;

namespace aspnetdemo2.domain.aerolineas {


    public class EditarAerolineaCommand : IRequest<bool> {
        public EditarAerolineaCommand(string id, string nombre, string paisOrigen, int calificacion)
        {
            Id = id;
            Nombre = nombre;
            PaisOrigen = paisOrigen;
            Calificacion = calificacion;
        }
        public EditarAerolineaCommand()
        {
            
        }

        public string Id { get;  set; }
        public string Nombre { get; set; }
        public string PaisOrigen { get; set; }
        public int Calificacion {get; set;}
        

    }


    public class EditarAerolineaCommandValidator : AbstractValidator<EditarAerolineaCommand>
    {
        public EditarAerolineaCommandValidator()
        {
             RuleFor(m => m.Nombre).NotEmpty().MinimumLength(3);
            RuleFor(m => m.PaisOrigen).NotEmpty().MinimumLength(3);
            RuleFor(m => m.Calificacion).NotEmpty()
                        .Must( p => p >= 0);
        }
    }


    public class EditarAerolineaCommandHandler
           : IRequestHandler<EditarAerolineaCommand, bool>
    {
        private readonly IMongoCollection<Aerolinea> aerolineas;

        public EditarAerolineaCommandHandler(IEstudiantesDatabaseSettings settings)
        {
            var cliente = new MongoClient(settings.ConnectionString);
            var db = cliente.GetDatabase(settings.DatabaseName);

            aerolineas = db.GetCollection<Aerolinea>("Aerolineas");
        }

        public async Task<bool> Handle(EditarAerolineaCommand request, CancellationToken cancellationToken)
        {
            var mgAerolinea = (await aerolineas
                    .FindAsync<Aerolinea>( aero => aero.Id == request.Id)
            ).FirstOrDefault();
            mgAerolinea.Nombre = request.Nombre;
            mgAerolinea.PaisOrigen = request.PaisOrigen;
            mgAerolinea.Calificacion = request.Calificacion;
             aerolineas.ReplaceOne(aero => aero.Id == mgAerolinea.Id,
                mgAerolinea
              );
            
            return true;
        }
    }

    

}