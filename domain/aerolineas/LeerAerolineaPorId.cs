using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;

namespace aspnetdemo2.domain.aerolineas {


    public class LeerAerolineaPorId : IRequest<AerolineaDetalleModel> {
        public LeerAerolineaPorId(string id)
        {
            Id = id;
        }

        public string Id { get; }

     }

     public class AerolineaDetalleModel {
          public string   Id { get; set; }
        public string   Nombre { get; set; }
        public string PaisOrigen { get; set; }
        public int Calificacion { get; set; }
    
     }

    public class LeerAerolineaPorIdHandler
           : IRequestHandler<LeerAerolineaPorId, AerolineaDetalleModel>
    {
        private readonly IMongoCollection<Aerolinea> aerolineas;

        public LeerAerolineaPorIdHandler(IEstudiantesDatabaseSettings settings)
        {
            var cliente = new MongoClient(settings.ConnectionString);
            var db = cliente.GetDatabase(settings.DatabaseName);

            aerolineas = db.GetCollection<Aerolinea>("Aerolineas");
        }

        public async Task<AerolineaDetalleModel> Handle(LeerAerolineaPorId request, CancellationToken cancellationToken)
        {
            
            var mgAerolinea = (await aerolineas
                    .FindAsync<Aerolinea>( 
                            aero => aero.Id == request.Id
                            )
            ).FirstOrDefault();
            
           
           if(mgAerolinea is Aerolinea m ){
            return new AerolineaDetalleModel() {
                Id = m.Id,
                Nombre = m.Nombre,
                PaisOrigen = m.PaisOrigen,
                Calificacion = m.Calificacion
            };
           }else{
               return null;
           }
        }
    }

}