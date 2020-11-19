using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MongoDB.Driver;

namespace aspnetdemo2.domain.aerolineas {


    public class BorrarAerolineaCommand : IRequest<bool> {
        public BorrarAerolineaCommand(string id)
        {
            Id = id;
        }
        public BorrarAerolineaCommand()
        {
            
        }

        public string Id { get;  set; }
        

    }


    public class BorrarAerolineaCommandValidator : AbstractValidator<BorrarAerolineaCommand>
    {
        public BorrarAerolineaCommandValidator()
        {
          
        }
    }


    public class BorrarAerolineaCommandHandler
           : IRequestHandler<BorrarAerolineaCommand, bool>
    {
        private readonly IMongoCollection<Aerolinea> aerolineas;

        public BorrarAerolineaCommandHandler(IEstudiantesDatabaseSettings settings)
        {
            var cliente = new MongoClient(settings.ConnectionString);
            var db = cliente.GetDatabase(settings.DatabaseName);

            aerolineas = db.GetCollection<Aerolinea>("Aerolineas");
        }

        public async Task<bool> Handle(BorrarAerolineaCommand request, CancellationToken cancellationToken)
        {
           await aerolineas
                    .DeleteOneAsync( aero => aero.Id == request.Id);
            
            return true;
        }
    }

    

}