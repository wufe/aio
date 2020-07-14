using AutoMapper;
using Ghoul.Application.Model;
using Ghoul.Application.Model.Build;
using Ghoul.Application.Model.Commands;
using Ghoul.Presentation.Model.Build;

namespace Ghoul.Presentation.Web.Configuration.Mapping {

    // Questo PRESENTATION PROFILE (profilo automapper lato presentazionale)
    // estende la classe di automapper "Profile"
    public class PresentationApplicationMappingProfile : Profile { // <--- Automapper.Profile
        public PresentationApplicationMappingProfile()
        {
            // Le regole del profilo vengono applicate tramite il costruttore
            //
            // In questo caso ho diviso le regole in due metodi diversi,
            // per separazione concettuale
            CreateModelMappings();
            CreateCommandQueryMappings();
        }

        // Qui mappo i modelli presentazionali in "Presentation/Ghoul.Presentation.Model",
        // in modelli APPLICATIVO ("Application/Ghoul.Application.Model")
        private void CreateModelMappings() {
            // La regola molto banalmente consiste nel dire il tipo di partenza (source)
            // e il tipo di arriva (destination)
            //
            // Automapper si occuperà di mappare AUTOMATICAMENTE i campi di queste due classi
            // se hanno le proprietà con lo stesso nome (case insensitive)
            //
            // Se le proprietà dei modelli non hanno gli stessi nomi,
            // o si vuole applicare delle regole particolari, si aggiungono istruzioni, del tipo:
            CreateMap<CreateBuildInputModel, BuildApplicationModel>();

            // CreateMap<CreateBuildInputModel, BuildApplicationModel>()
            //     .ForMember(destinazione => destinazione.Name, opzioni => opzioni.MapFrom(sorgente => sorgente.Name + "Ciccio"));
            //
            // Questa regola mapperà qualsiasi proprietà "Name" nel CreateBuildInputModel,
            // nella proprietà "Name" del BuildApplicationModel, concatenando però la parola "Ciccio"
            // CreateBuildInputModel { Name = "ABC" } -> BuildApplicationModel { Name = "ABCCiccio" }
            //
            // Le regole possono (e sono talvolta) più complicate di così e coprono un sacco di casi
            //
            // Molte volte basta invece indicare sorgente e destinazione, e automapper provvederà a mappare
            // proprietà della sorgente con proprietà della destinazione in base al loro nome.
        }

        private void CreateCommandQueryMappings() {
            // In questo caso mappo i modelli presentazionali in Presentation/Ghoul.Presentation.Model
            // con i "COMMAND" lato applicativo (Application/Ghoul.Application.Model)
            //
            // Un command è praticamente un ordine di scrittura.
            // "CreateBuildCommand" = Creami questa build
            //
            // In questo caso entrambi gli oggetti hanno le stesse proprietà
            //
            // Avendo le stesse proprietà con lo stesso nome e tipo, il mapping avviene facilmente
            CreateMap<CreateBuildInputModel, CreateBuildCommand>();
        }
    }
}