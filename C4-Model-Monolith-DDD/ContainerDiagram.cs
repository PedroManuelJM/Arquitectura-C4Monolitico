using Structurizr;

namespace C4_Model_Monolith_DDD
{
	public class ContainerDiagram
	{
		private readonly C4 c4;
		private readonly ContextDiagram contextDiagram;

    public Container AppWebMonitoreo { get; private set; } = null!;
    public Container AppMovilVecinal { get; private set; } = null!;
    public Container AppMovilPatrullaje { get; private set; } = null!;

    public Container ApiRest { get; private set; } = null!;
    public Container Database { get; private set; } = null!;
		public Container ReplicaDatabase { get; private set; } = null!;

		public ContainerDiagram(C4 c4, ContextDiagram contextDiagram)
		{
			this.c4 = c4;
			this.contextDiagram = contextDiagram;
		}

		public void Generate() {
			AddContainers();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddContainers()
		{

     AppWebMonitoreo = contextDiagram.MonitoreoPatrullajeInteligente.AddContainer("Mapper Monitoreo", "Permite la supervisión ,asignación, priorización de incidencia reportadas por los ciudadanos.","Angular");
     AppMovilVecinal = contextDiagram.MonitoreoPatrullajeInteligente.AddContainer("Mapper Ciudadano", "Permite el registro de incidencias y seguimiento de atención.","Flutter");
     AppMovilPatrullaje = contextDiagram.MonitoreoPatrullajeInteligente.AddContainer("Mapper Patrullaje", "Permite que el sereno realize las atención de las incidencias reportadas por los ciudadanos.","Flutter");

     ApiRest = contextDiagram.MonitoreoPatrullajeInteligente.AddContainer("API REST", "", "AWS Lambda + API GATEWAY + Spring Boot");

     Database = contextDiagram.MonitoreoPatrullajeInteligente.AddContainer("Database", "Permite el almacenamiento y consulta de datos en tiempo real", "AWS DynamoDB");
     ReplicaDatabase = contextDiagram.MonitoreoPatrullajeInteligente.AddContainer("Replica DB", "Respaldo de la base de datos", "PostgreSQL");

    }

    private void AddRelationships() {

      contextDiagram.SupervisorGeneral.Uses(AppWebMonitoreo, "Supervisa y monitorea");
      contextDiagram.PersonalAdministrativo.Uses(AppWebMonitoreo, "Asigna , controla y monitorea");
      contextDiagram.Ciudadano.Uses(AppMovilVecinal, "registra , consulta , informa , captura , notifica y visualiza");
      contextDiagram.Serenazgo.Uses(AppMovilPatrullaje, "visualiza , notifica y adjunta");

      AppWebMonitoreo.Uses(ApiRest, "API Request", "JSON/HTTPS");
      AppMovilVecinal.Uses(ApiRest, "API Request", "JSON/HTTPS");
      AppMovilPatrullaje.Uses(ApiRest, "API Request", "JSON/HTTPS");

      ApiRest.Uses(Database, "API Request", "");
      ApiRest.Uses(ReplicaDatabase, "Replica", "");
      ApiRest.Uses(contextDiagram.ServicioMapa, "API Request", "JSON/HTTPS");
      ApiRest.Uses(contextDiagram.ServicioSeguridad, "API Request", "JSON/HTTPS");

    }

		private void ApplyStyles() {
			SetTags();

			Styles styles = c4.ViewSet.Configuration.Styles;

      styles.Add(new ElementStyle(nameof(AppWebMonitoreo)) { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
      styles.Add(new ElementStyle(nameof(AppMovilVecinal)) { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
      styles.Add(new ElementStyle(nameof(AppMovilPatrullaje)) { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });

      styles.Add(new ElementStyle(nameof(ApiRest)) { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
      styles.Add(new ElementStyle(nameof(Database)) { Shape = Shape.Cylinder, Background = "#FF7F50", Color = "#ffffff", Icon = "" });
      styles.Add(new ElementStyle(nameof(ReplicaDatabase)) { Shape = Shape.Cylinder, Background = "#FF7F50", Color = "#ffffff", Icon = "" });

    }

    private void SetTags()
		{
			AppWebMonitoreo.AddTags(nameof(AppWebMonitoreo));
			AppMovilVecinal.AddTags(nameof(AppMovilVecinal));
			AppMovilPatrullaje.AddTags(nameof(AppMovilPatrullaje));
			ApiRest.AddTags(nameof(ApiRest));
      Database.AddTags(nameof(Database));
			ReplicaDatabase.AddTags(nameof(ReplicaDatabase));
		}

		private void CreateView() {
			ContainerView containerView = c4.ViewSet.CreateContainerView(contextDiagram.MonitoreoPatrullajeInteligente, "Contenedor", "Diagrama de contenedores");
			containerView.AddAllElements();
			containerView.DisableAutomaticLayout();
		}
	}
}
