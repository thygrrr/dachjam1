using Godot;

namespace jam.code;

public static class GodotNotStupidExtensions
{
    public static (Vector3 Origin, Vector3 Direction) MouseRay(this Camera3D self)
    {
        var mousePos = self.GetViewport().GetMousePosition();
        var rayOrigin = self.ProjectRayOrigin(mousePos);
        var rayDirection = self.ProjectRayNormal(mousePos);
        return (rayOrigin, rayDirection);
    }
    
    public static bool IntersectsRay(this Plane self, Vector3 origin, Vector3 direction, out Vector3 intersection)
    {
        var intersect = self.IntersectsRay(origin, direction);
        if (intersect.HasValue)
        {
            intersection = intersect.Value;
            return true;
        }

        intersection = default;
        return false;
    }
}