
public class CubeController : AbstractController<Cube>
{
    protected override void OnViewUpdated(Cube model)
    {
        _model.Position = model.Position;
        _model.OnUpdated();
    }
}
