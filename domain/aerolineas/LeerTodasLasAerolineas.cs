using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;

namespace aspnetdemo2.domain.aerolineas {


    public class LeerTodasLasAerolineas : IRequest<List<AerolineaIndexModel>> {

     }

     public class AerolineaIndexModel {

        public string Id {get; set;}
        public string   Nombre { get; set; }
        public string PaisOrigen { get; set; }
        public int Calificacion { get; set; }

     }

    public class LeerTodasLasAerolineasHandler : IRequestHandler<LeerTodasLasAerolineas,List<AerolineaIndexModel>>
    {
        private readonly IMongoCollection<Aerolinea> aerolineas;

        public LeerTodasLasAerolineasHandler(IEstudiantesDatabaseSettings settings)
        {
            var cliente = new MongoClient(settings.ConnectionString);
            var db = cliente.GetDatabase(settings.DatabaseName);

            aerolineas = db.GetCollection<Aerolinea>("Aerolineas");
        }

        public async Task<List<AerolineaIndexModel>> Handle(LeerTodasLasAerolineas request, CancellationToken cancellationToken)
        {
            var resultado = await aerolineas.FindAsync<Aerolinea>(aero => true);
            var res = resultado.ToList().Select(aero => 
                    new AerolineaIndexModel() {
                        Id = aero.Id,
                        Nombre = aero.Nombre,
                        PaisOrigen = aero.PaisOrigen,
                        Calificacion = aero.Calificacion
                    }
            );

            return res.ToList();
        }
    }

}