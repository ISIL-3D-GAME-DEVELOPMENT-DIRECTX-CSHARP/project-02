using Sesion2_Lab01.com.isil.graphics;
using SharpDX;
using System;


namespace math {
public class NFrustumView {

    private NPlane3D[] mPlanes;

    public NFrustumView() {
        mPlanes = new NPlane3D[6];
    }

    public bool CheckPoint(float x, float y, float z) {
        NVector4 point = NVector4.Zero;
        point.X = x;
        point.Y = y;
        point.Z = z;

	    // Check if the point is inside all six planes of the view frustum.
        for (int i = 0; i < 6; i++) {
            NPlane3D thisPlane = mPlanes[i];
            
            if (NPlane3D.PlaneDotCoord(ref thisPlane, ref point) < 0.0f) {
			    return false;
		    }
	    }

	    return true;
    }

    public bool CheckPoint(NVector3 point) {
        return this.CheckPoint(point.X, point.Y, point.Z);
    }

    public bool CheckPoint(ref NVector3 point) {
        return this.CheckPoint(point.X, point.Y, point.Z);
    }

    public bool CheckCube(float xCenter, float yCenter, float zCenter, float radius) {
	    // Check if any one point of the cube is in the view frustum.
	    for(int i = 0; i < 6; i++) {
            NPlane3D thisPlane = mPlanes[i];

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter - radius), 
                (yCenter - radius), (zCenter - radius)) >= 0.0f) {
			    continue;
		    }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter + radius), 
                (yCenter - radius), (zCenter - radius)) >= 0.0f) {
			    continue;
		    }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter - radius), 
                (yCenter + radius), (zCenter - radius)) >= 0.0f) {
			    continue;
		    }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter + radius), 
                (yCenter + radius), (zCenter - radius)) >= 0.0f) {
			    continue;
		    }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter - radius), 
                (yCenter - radius), (zCenter + radius)) >= 0.0f) {
			    continue;
		    }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter + radius), 
                (yCenter - radius), (zCenter + radius)) >= 0.0f) {
			    continue;
		    }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter - radius), 
                (yCenter + radius), (zCenter + radius)) >= 0.0f) {
			    continue;
		    }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter + radius), 
                (yCenter + radius), (zCenter + radius)) >= 0.0f) {
			    continue;
		    }

		    return false;
	    }

	    return true;
    }

    public bool CheckCube(float xCenter, float yCenter, float zCenter, 
        float width, float height, float depth) {

        width = width * 0.5f;
        height = height * 0.5f;
        depth = depth * 0.5f;

        // Check if any one point of the cube is in the view frustum.
        for (int i = 0; i < 6; i++) {
            NPlane3D thisPlane = mPlanes[i];

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter - width),
                (yCenter - height), (zCenter - depth)) >= 0.0f) {
                continue;
            }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter + width),
                (yCenter - height), (zCenter - depth)) >= 0.0f) {
                continue;
            }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter - width),
                (yCenter + height), (zCenter - depth)) >= 0.0f) {
                continue;
            }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter + width),
                (yCenter + height), (zCenter - depth)) >= 0.0f) {
                continue;
            }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter - width),
                (yCenter - height), (zCenter + depth)) >= 0.0f) {
                continue;
            }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter + width),
                (yCenter - height), (zCenter + depth)) >= 0.0f) {
                continue;
            }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter - width),
                (yCenter + height), (zCenter + depth)) >= 0.0f) {
                continue;
            }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter + width),
                (yCenter + height), (zCenter + depth)) >= 0.0f) {
                continue;
            }

            return false;
        }

        return true;
    }

    public bool CheckCube(PrimitiveCube3D cube)
    {
        return this.CheckCube(cube.X, cube.Y, cube.Z, cube.SizeX, cube.SizeY, cube.SizeZ);
    }

    public bool CheckSphere(float xCenter, float yCenter, float zCenter, float radius) {
        NVector4 point = NVector4.Zero;
        point.X = xCenter;
        point.Y = yCenter;
        point.Z = zCenter;

	    // Check if the radius of the sphere is inside the view frustum.
	    for(int i = 0; i < 6; i++) {
            NPlane3D thisPlane = mPlanes[i];

            if (NPlane3D.PlaneDotCoord(ref thisPlane, ref point) < -radius) {
			    return false;
		    }
	    }

	    return true;
    }

    /*public bool CheckSphere(NPrimitiveSphere3D sphere) {
        return this.CheckSphere(sphere.X, sphere.Y, sphere.Z, sphere.Radius);
    }*/

    public bool CheckRectangle(float xCenter, float yCenter, float zCenter, 
        float xSize, float ySize, float zSize) {
        
	    // Check if any of the 6 planes of the rectangle are inside the view frustum.
	    for(int i = 0; i < 6; i++) {
            NPlane3D thisPlane = mPlanes[i];

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter - xSize), 
                (yCenter - ySize), (zCenter - zSize)) >= 0.0f) {
			    continue;
		    }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter + xSize), 
                (yCenter - ySize), (zCenter - zSize)) >= 0.0f) {
			    continue;
		    }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter - xSize), 
                (yCenter + ySize), (zCenter - zSize)) >= 0.0f) {
			    continue;
		    }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter - xSize), 
                (yCenter - ySize), (zCenter + zSize)) >= 0.0f) {
			    continue;
		    }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter + xSize), 
                (yCenter + ySize), (zCenter - zSize)) >= 0.0f) {
			    continue;
		    }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter + xSize), 
                (yCenter - ySize), (zCenter + zSize)) >= 0.0f) {
			    continue;
		    }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter - xSize), 
                (yCenter + ySize), (zCenter + zSize)) >= 0.0f) {
			    continue;
		    }

            if (NPlane3D.PlaneDotCoord(ref thisPlane, (xCenter + xSize), 
                (yCenter + ySize), (zCenter + zSize)) >= 0.0f) {
			    continue;
		    }

		    return false;
	    }

	    return true;
    }

    public void ComputeFrustum(float screenDepth, ref Matrix projection, ref Matrix view) {
        float r = 0f;
        float zMinimum = 0f;
        Matrix matrix = Matrix.Identity;

        float oldProjection_M33 = projection.M33;
        float oldProjection_M43 = projection.M43;

        // Calculate the minimum Z distance in the frustum.
        zMinimum = -projection.M43 / projection.M33;
        r = screenDepth / (screenDepth - zMinimum);
        projection.M33 = r;
        projection.M43 = -r * zMinimum;

        // Create the frustum matrix from the view matrix and updated projection matrix.
        Matrix.Multiply(ref view, ref projection, out matrix);

        // Calculate near plane of frustum.
        NPlane3D plane0 = mPlanes[0];
        plane0.A = matrix.M14 + matrix.M13;
        plane0.B = matrix.M24 + matrix.M23;
        plane0.C = matrix.M34 + matrix.M33;
        plane0.D = matrix.M44 + matrix.M43;

        NPlane3D.Normalize(ref plane0);

        // Calculate far plane of frustum.
        NPlane3D plane1 = mPlanes[1];
        plane1.A = matrix.M14 - matrix.M13;
        plane1.B = matrix.M24 - matrix.M23;
        plane1.C = matrix.M34 - matrix.M33;
        plane1.D = matrix.M44 - matrix.M43;

        NPlane3D.Normalize(ref plane1);

        // Calculate left plane of frustum.
        NPlane3D plane2 = mPlanes[2];
        plane2.A = matrix.M14 + matrix.M11;
        plane2.B = matrix.M24 + matrix.M21;
        plane2.C = matrix.M34 + matrix.M31;
        plane2.D = matrix.M44 + matrix.M41;
                          
        NPlane3D.Normalize(ref plane2);

        // Calculate right plane of frustum.
        NPlane3D plane3 = mPlanes[3];
        plane3.A = matrix.M14 - matrix.M11;
        plane3.B = matrix.M24 - matrix.M21;
        plane3.C = matrix.M34 - matrix.M31;
        plane3.D = matrix.M44 - matrix.M41;

        NPlane3D.Normalize(ref plane3);

        // Calculate top plane of frustum.
        NPlane3D plane4 = mPlanes[4];
        plane4.A = matrix.M14 - matrix.M12;
        plane4.B = matrix.M24 - matrix.M22;
        plane4.C = matrix.M34 - matrix.M32;
        plane4.D = matrix.M44 - matrix.M42;

        NPlane3D.Normalize(ref plane4);

        // Calculate bottom plane of frustum.
        NPlane3D plane5 = mPlanes[5];
        plane5.A = matrix.M14 + matrix.M12;
        plane5.B = matrix.M24 + matrix.M22;
        plane5.C = matrix.M34 + matrix.M32;
        plane5.D = matrix.M44 + matrix.M42;

        NPlane3D.Normalize(ref plane5);

        mPlanes[0] = plane0;
        mPlanes[1] = plane1;
        mPlanes[2] = plane2;
        mPlanes[3] = plane3;
        mPlanes[4] = plane4;
        mPlanes[5] = plane5;

        // return projection matrix as before
        projection.M33 = oldProjection_M33;
        projection.M43 = oldProjection_M43;
    }
}
}
