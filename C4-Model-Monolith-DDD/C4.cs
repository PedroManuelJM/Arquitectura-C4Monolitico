using Structurizr;
using Structurizr.Api;

namespace C4_Model_Monolith_DDD
{
  public class C4
  {
    private readonly long workspaceId = 0;
    private readonly string apiKey = "";
    private readonly string apiSecret = "";

    public StructurizrClient StructurizrClient { get; }
    public Workspace Workspace { get; }
    public Model Model { get; }
    public ViewSet ViewSet { get; }

    public C4()
    {
      string workspaceName = "Sistema de Patrullaje Inteligente Distrital  - Monolith DDD";
      string workspaceDescription = "Sistema de Patrullaje Inteligente Distrital";
      StructurizrClient = new StructurizrClient(apiKey, apiSecret);
      Workspace = new Workspace(workspaceName, workspaceDescription);
      Model = Workspace.Model;
      ViewSet = Workspace.Views;
    }

    public void Generate()
    {
      ContextDiagram contextDiagram = new(this);
      ContainerDiagram containerDiagram = new(this, contextDiagram);
      // MonitoringComponentDiagram monitoringComponentDiagram = new(this, contextDiagram, containerDiagram);
      contextDiagram.Generate();
      containerDiagram.Generate();
      //monitoringComponentDiagram.Generate();
      PutWorkspace();
    }

    private void PutWorkspace()
    {
      StructurizrClient.UnlockWorkspace(workspaceId);
      StructurizrClient.PutWorkspace(workspaceId, Workspace);
    }
  }
}
