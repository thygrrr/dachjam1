class_name Smooth

static func Pos(current : Vector3, target : Vector3, smoothTime : float, deltaTime : float, velocity : Array[Vector3]) -> Vector3:
	if smoothTime == 0:
		velocity[0] = Vector3.ZERO
		return target

	var linear_velocity = velocity[0]

	var omega = 2.0 / smoothTime

	var x = omega * deltaTime;
	var approx_exp = 1.0 / (1.0 + x + 0.48 * x * x + 0.235 * x * x * x);

	var change = current - target;
	var originalTo = target;

	# Clamp maxSpeed
	#var maxChange = maxSpeed * smoothTime;
	#change = clamp(change, -maxChange, maxChange);
	target = current - change;

	var temp = (linear_velocity + omega * change) * deltaTime
	linear_velocity = (linear_velocity - omega * temp) * approx_exp
	var output = target + (change + temp) * approx_exp

	# Prevent overshooting - FIXME - probably needs to treat all components separately.
	if (originalTo.x > current.x) == (output.x > originalTo.x):
		output.x = originalTo.x
		linear_velocity.x = (output.x - originalTo.x) / deltaTime
	if (originalTo.y > current.y) == (output.y > originalTo.y):
		output.y = originalTo.y
		linear_velocity.y = (output.y - originalTo.y) / deltaTime
	if (originalTo.z > current.z) == (output.z > originalTo.z):
		output.z = originalTo.z
		linear_velocity.z = (output.z - originalTo.z) / deltaTime

	# Write state back
	velocity[0] = linear_velocity

	return output;


static func Eulers(current : Vector3, target : Vector3, smoothTime : float, deltaTime : float, velocity : Array[Vector3]) -> Vector3:
	if smoothTime == 0:
		return target

	var angular_velocity = velocity[0]

	var omega = 2.0 / smoothTime

	var x = omega * deltaTime;
	var approx_exp = 1.0 / (1.0 + x + 0.48 * x * x + 0.235 * x * x * x);

	var change = current - target;
	#var originalTo = target;

	# Clamp maxSpeed
	#var maxChange = maxSpeed * smoothTime;
	#change = clamp(change, -maxChange, maxChange);
	target = current - change;

	var temp = (angular_velocity + omega * change) * deltaTime
	angular_velocity = (angular_velocity - omega * temp) * approx_exp
	var output = target + (change + temp) * approx_exp

	# Prevent overshooting - FIXME - probably needs to treat all components separately.

	#if (originalTo.x > current.x) == (output.x > originalTo.x):
	#	output.x = originalTo.x
	#	angular_velocity.x = (output.x - originalTo.x) / deltaTime
	#if (originalTo.y > current.y) == (output.y > originalTo.y):
	#	output.y = originalTo.y
	#	angular_velocity.y = (output.y - originalTo.y) / deltaTime
	#if (originalTo.z > current.z) == (output.z > originalTo.z):
	#output.z = originalTo.z
	#	angular_velocity.z = (output.z - originalTo.z) / deltaTime

	velocity[0] = angular_velocity
	return output;
