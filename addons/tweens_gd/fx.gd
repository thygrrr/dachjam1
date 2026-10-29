# SPDX-License-Identifier: MIT
# SPDX-FileCopyrightText: 2026 Moritz Voss
extends TweensGdFX
## Creates reusable, deterministic progress-to-offset Callables.
##
## Scalar functions work as [code]ease_function[/code]. Vector and quaternion functions
## supply offsets to [code]on_update[/code] or custom interpolators. Frequency is per tween,
## not per second. Invalid configuration reports an error and returns an empty Callable.
## C# equivalent: [code]Tweens.FX[/code], returning [code]Func[/code] samplers.
