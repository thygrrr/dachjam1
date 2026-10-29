# SPDX-License-Identifier: MIT
# SPDX-FileCopyrightText: 2026 Moritz Voss
class_name TweensGdTypes
extends RefCounted
## Timing, mode, state, and completion-reason constants used by every definition.
##
## C# counterparts: TweenOptions.Infinite, FillMode, TweenProcessMode, TweenPauseMode, TweenState, Reason, and EaseType.

## Repeat indefinitely until cancelled. Used by [code]definition.repeats[/code].
const INFINITE := -1
## Working color coordinates, independent of easing and curve interpolation.
enum ColorSpace { OKLAB, SRGB, LINEAR_RGB }
## Color opacity handling. Premultiplication is internal; Godot receives straight alpha.
enum AlphaMode { PREMULTIPLIED, STRAIGHT }
## RGB encoding expected by the target Godot API. Alpha remains linear.
enum ColorEncoding { SRGB, LINEAR_RGB }
## Segment value interpolation, independently of the tween's global easing.
enum Interpolation { SMOOTH = -2, LINEAR = -3, STEP = -4 }
## Value behavior during delay and after natural completion. Cancellation does not restore the initial value.
enum Fill {
	## Leave the property untouched during delay; restore its initial value on natural completion.
	NONE = 0,
	## Apply from_value during delay; restore the initial value on natural completion.
	APPLY_FROM_DURING_DELAY = 1,
	## Keep the final value on natural completion; leave the property untouched during delay.
	RETAIN_FINAL_VALUE = 2,
	## Apply from_value during delay and keep the final value on natural completion.
	BOTH = 3,
}
## The update phase used by playback.
enum Process {
	## Advance on process ticks.
	PROCESS,
	## Advance on physics ticks.
	PHYSICS,
}
## How playback responds to scene and owner pausing.
enum Pause {
	## Follow the owner's can_process; without an owner, follow scene-tree pause.
	BOUND,
	## Follow scene-tree pause, independently of the owner's process mode.
	SCENE_TREE,
	## Ignore scene and owner pausing. Explicit handle pauses still apply.
	ALWAYS,
}
## Current timeline state, independently of explicit pausing.
enum State {
	## Waiting for the initial delay.
	DELAYED,
	## Advancing a forward or return leg.
	PLAYING,
	## Holding between legs or cycles.
	INTERVAL,
	## Ended naturally.
	COMPLETED,
	## Stopped before natural completion.
	CANCELLED,
	## Stopped because playback or a callback failed.
	FAULTED,
}
## Why playback ended. FAILED and WAIT_CANCELLED represent errors that C# reports as exceptions.
enum Reason {
	## Every scheduled cycle completed.
	COMPLETED,
	## Playback was explicitly cancelled.
	CANCELLED,
	## The target was queued for deletion, or found freed. [method Object.free] removes a node from the scene tree
	## before deleting it, so a node that owns its handle, the default for node targets, reports OWNER_EXITED instead.
	TARGET_FREED,
	## The owner left the scene tree, or a separate owner node was queued for deletion.
	OWNER_EXITED,
	## The scheduler or scene tree was disposed.
	RUNNER_DISPOSED,
	## Playback or its start failed. Inspect the handle or group error.
	FAILED,
	## Only the wait was cancelled. Playback continues.
	WAIT_CANCELLED,
}
## Legacy easing identifiers. Prefer [In], [Out], and [InOut]; do not combine these constants with [code]|[/code].
enum Ease {
	## Legacy LINEAR curve.
	LINEAR = 0,
	## Legacy SINE_IN curve.
	SINE_IN = 10,
	## Legacy SINE_OUT curve.
	SINE_OUT,
	## Legacy SINE_IN_OUT curve.
	SINE_IN_OUT,
	## Legacy QUAD_IN curve.
	QUAD_IN = 20,
	## Legacy QUAD_OUT curve.
	QUAD_OUT,
	## Legacy QUAD_IN_OUT curve.
	QUAD_IN_OUT,
	## Legacy CUBIC_IN curve.
	CUBIC_IN = 30,
	## Legacy CUBIC_OUT curve.
	CUBIC_OUT,
	## Legacy CUBIC_IN_OUT curve.
	CUBIC_IN_OUT,
	## Legacy QUART_IN curve.
	QUART_IN = 40,
	## Legacy QUART_OUT curve.
	QUART_OUT,
	## Legacy QUART_IN_OUT curve.
	QUART_IN_OUT,
	## Legacy QUINT_IN curve.
	QUINT_IN = 50,
	## Legacy QUINT_OUT curve.
	QUINT_OUT,
	## Legacy QUINT_IN_OUT curve.
	QUINT_IN_OUT,
	## Legacy EXPO_IN curve.
	EXPO_IN = 60,
	## Legacy EXPO_OUT curve.
	EXPO_OUT,
	## Legacy EXPO_IN_OUT curve.
	EXPO_IN_OUT,
	## Legacy CIRC_IN curve.
	CIRC_IN = 70,
	## Legacy CIRC_OUT curve.
	CIRC_OUT,
	## Legacy CIRC_IN_OUT curve.
	CIRC_IN_OUT,
	## Legacy BACK_IN curve.
	BACK_IN = 80,
	## Legacy BACK_OUT curve.
	BACK_OUT,
	## Legacy BACK_IN_OUT curve.
	BACK_IN_OUT,
	## Legacy ELASTIC_IN curve.
	ELASTIC_IN = 90,
	## Legacy ELASTIC_OUT curve.
	ELASTIC_OUT,
	## Legacy ELASTIC_IN_OUT curve.
	ELASTIC_IN_OUT,
	## Legacy BOUNCE_IN curve.
	BOUNCE_IN = 100,
	## Legacy BOUNCE_OUT curve.
	BOUNCE_OUT,
	## Legacy BOUNCE_IN_OUT curve.
	BOUNCE_IN_OUT,
	## Legacy SMOOTH_STEP curve.
	SMOOTH_STEP = 110,
	## Legacy SMOOTHER_STEP curve.
	SMOOTHER_STEP = 120,
}
