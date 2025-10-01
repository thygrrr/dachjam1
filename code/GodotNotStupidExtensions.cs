using Godot;

namespace jam;

public static class GodotNotStupidExtensions
{
    public static (Vector3 Origin, Vector3 Direction) MouseRay(this Camera3D self)
    {
        var mousePos = self.GetViewport().GetMousePosition();
        var rayOrigin = self.ProjectRayOrigin(mousePos);
        var rayDirection = self.ProjectRayNormal(mousePos);
        return (rayOrigin, rayDirection);
    }
    
    public static Vector3? IntersectsRay(this Plane self, (Vector3 origin, Vector3 direction) ray) => self.IntersectsRay(ray.origin, ray.direction);
}