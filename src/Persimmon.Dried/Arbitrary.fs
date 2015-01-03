﻿namespace Persimmon.Dried

type NonShrinkerArbitrary<'T> = {
  Gen: Gen<'T>
  PrettyPrinter: 'T -> Pretty
}

type Arbitrary<'T> = {
  Gen: Gen<'T>
  Shrinker: Shrink<'T>
  PrettyPrinter: 'T -> Pretty
}
with
  member this.NonShrinker: NonShrinkerArbitrary<'T> = {
    Gen = this.Gen
    PrettyPrinter = this.PrettyPrinter
  }

[<RequireQualifiedAccess>]
module Arb =

  let unit = {
    Gen = Gen.constant ()
    Shrinker = Shrink.shrinkAny
    PrettyPrinter = fun () -> Pretty(fun _ -> "unit")
  }

  let bool = {
    Gen = Gen.elements [ true; false ]
    Shrinker = Shrink.shrinkAny
    PrettyPrinter = Pretty.prettyAny
  }

  open FsRandom
  open System

  let byte = {
    Gen = Gen.choose (Statistics.uniformDiscrete (int Byte.MinValue, int Byte.MaxValue)) |> Gen.map byte
    Shrinker = Shrink.shrinkByte
    PrettyPrinter = Pretty.prettyAny
  }

  let sbyte = {
    Gen = Gen.choose (Statistics.uniformDiscrete (int SByte.MinValue, int SByte.MaxValue)) |> Gen.map sbyte
    Shrinker = Shrink.shrinkSbyte
    PrettyPrinter = Pretty.prettyAny
  }

  let int16 = {
    Gen = Gen.choose (Statistics.uniformDiscrete (int Int16.MinValue, int Int16.MaxValue)) |> Gen.map int16
    Shrinker = Shrink.shrinkInt16
    PrettyPrinter = Pretty.prettyAny
  }

  let int = {
    Gen = Gen.choose (Statistics.uniformDiscrete (Int32.MinValue, Int32.MaxValue))
    Shrinker = Shrink.shrinkInt
    PrettyPrinter = Pretty.prettyAny
  }