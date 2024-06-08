using Structurizr;

namespace C4_Model_Monolith_DDD
{
  public class ContextDiagram
  {
    private readonly C4 c4;

    public SoftwareSystem MonitoreoPatrullajeInteligente { get; private set; } = null!;
    public SoftwareSystem ServicioMapa { get; private set; } = null!;
    public SoftwareSystem ServicioSeguridad{ get; private set; } = null!;

    public Person Ciudadano { get; private set; } = null!;
    public Person Serenazgo { get; private set; } = null!;
    public Person PersonalAdministrativo { get; private set; } = null!;
    public Person SupervisorGeneral { get; private set; } = null!;

    public ContextDiagram(C4 c4)
    {
      this.c4 = c4;
    }

    public void Generate()
    {
      AddSoftwareSystems();
      AddPeople();
      AddRelationships();
      ApplyStyles();
      CreateView();
    }

    private void AddSoftwareSystems()
    {

      MonitoreoPatrullajeInteligente = c4.Model.AddSoftwareSystem("Monitoreo Patrullaje Inteligente", "Sistema de monitoreo de Patrullaje Inteligente");
      ServicioMapa = c4.Model.AddSoftwareSystem("Servicios de Mapas", "Plataforma que brinda una REST API para obtener información geográfica.");
      ServicioSeguridad = c4.Model.AddSoftwareSystem("Servicios de seguridad", "Utiliza Servicios de Seguridad para la autenticación y gestión de usuarios,que incluye: Amazon Cognito, Autenticación con Gmail");

    }

    private void AddPeople()
    {
      Ciudadano = c4.Model.AddPerson("Ciudadano", "Ciudadano peruano.");
      Serenazgo = c4.Model.AddPerson("Serenazgo", "Encargado de la seguridad ciudadana del distrito");
      PersonalAdministrativo = c4.Model.AddPerson("Personal Administrativo", "Encargado de monitorear el sistema de patrullaje distrital ");
      SupervisorGeneral = c4.Model.AddPerson("Supervisor General", "Encargado de supervisar el Mapper Monitoreo");

    }

    private void AddRelationships()
    {
      PersonalAdministrativo.Uses(MonitoreoPatrullajeInteligente, "Permite al usuario supervisar y gestionar las incidencias reportadas");
      SupervisorGeneral.Uses(MonitoreoPatrullajeInteligente, "Permite al usuario supervisar el sistema y el monitoreo.");
      
      Ciudadano.Uses(MonitoreoPatrullajeInteligente, "Registra sus incidencias y seguimiento de su atención");
      Serenazgo.Uses(MonitoreoPatrullajeInteligente, "Permite al serenazgo asistir al punto reportado de las incidencias reportadas.");
      MonitoreoPatrullajeInteligente.Uses(ServicioMapa, "Usa");
      MonitoreoPatrullajeInteligente.Uses(ServicioSeguridad, "Usa");

    }

    private void ApplyStyles()
    {
      SetTags();

      Styles styles = c4.ViewSet.Configuration.Styles;

      styles.Add(new ElementStyle(nameof(MonitoreoPatrullajeInteligente)) { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
      styles.Add(new ElementStyle(nameof(ServicioMapa)) { Background = "#008080", Color = "#ffffff", Shape = Shape.RoundedBox });
      styles.Add(new ElementStyle(nameof(ServicioSeguridad)) { Background = "#cd6f70", Color = "#ffffff", Shape = Shape.RoundedBox });


      styles.Add(new ElementStyle(nameof(Ciudadano)) { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
      styles.Add(new ElementStyle(nameof(Serenazgo)) { Background = "#08427b", Color = "#ffffff", Shape = Shape.Person });
      styles.Add(new ElementStyle(nameof(PersonalAdministrativo)) { Background = "#facc2e", Color = "#ffffff", Shape = Shape.Person });
      styles.Add(new ElementStyle(nameof(SupervisorGeneral)) { Background = "#008f39", Color = "#ffffff", Shape = Shape.Person });

    }

    private void SetTags()
    {
      MonitoreoPatrullajeInteligente.AddTags(nameof(MonitoreoPatrullajeInteligente));
      ServicioMapa.AddTags(nameof(ServicioMapa));
      ServicioSeguridad.AddTags(nameof(ServicioSeguridad));

      Ciudadano.AddTags(nameof(Ciudadano));
      Serenazgo.AddTags(nameof(Serenazgo));
      PersonalAdministrativo.AddTags(nameof(PersonalAdministrativo));
      SupervisorGeneral.AddTags(nameof(SupervisorGeneral));

    }

    private void CreateView()
    {

      SystemContextView contextView = c4.ViewSet.CreateSystemContextView(MonitoreoPatrullajeInteligente, "Contexto", "Diagrama de contexto");

      contextView.AddAllSoftwareSystems();
      contextView.AddAllPeople();
      contextView.DisableAutomaticLayout();
    }
  }
}
