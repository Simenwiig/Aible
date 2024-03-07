// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: mediapipe/calculators/core/packet_thinner_calculator.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Mediapipe {

  /// <summary>Holder for reflection information generated from mediapipe/calculators/core/packet_thinner_calculator.proto</summary>
  public static partial class PacketThinnerCalculatorReflection {

    #region Descriptor
    /// <summary>File descriptor for mediapipe/calculators/core/packet_thinner_calculator.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PacketThinnerCalculatorReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CjptZWRpYXBpcGUvY2FsY3VsYXRvcnMvY29yZS9wYWNrZXRfdGhpbm5lcl9j",
            "YWxjdWxhdG9yLnByb3RvEgltZWRpYXBpcGUaJG1lZGlhcGlwZS9mcmFtZXdv",
            "cmsvY2FsY3VsYXRvci5wcm90byLzAgoeUGFja2V0VGhpbm5lckNhbGN1bGF0",
            "b3JPcHRpb25zElIKDHRoaW5uZXJfdHlwZRgBIAEoDjI1Lm1lZGlhcGlwZS5Q",
            "YWNrZXRUaGlubmVyQ2FsY3VsYXRvck9wdGlvbnMuVGhpbm5lclR5cGU6BUFT",
            "WU5DEhEKBnBlcmlvZBgCIAEoAzoBMRISCgpzdGFydF90aW1lGAMgASgDEhAK",
            "CGVuZF90aW1lGAQgASgDEiQKFnN5bmNfb3V0cHV0X3RpbWVzdGFtcHMYBSAB",
            "KAg6BHRydWUSIAoRdXBkYXRlX2ZyYW1lX3JhdGUYBiABKAg6BWZhbHNlIiIK",
            "C1RoaW5uZXJUeXBlEgkKBUFTWU5DEAESCAoEU1lOQxACMlgKA2V4dBIcLm1l",
            "ZGlhcGlwZS5DYWxjdWxhdG9yT3B0aW9ucxiE2MqJASABKAsyKS5tZWRpYXBp",
            "cGUuUGFja2V0VGhpbm5lckNhbGN1bGF0b3JPcHRpb25z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Mediapipe.CalculatorReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Mediapipe.PacketThinnerCalculatorOptions), global::Mediapipe.PacketThinnerCalculatorOptions.Parser, new[]{ "ThinnerType", "Period", "StartTime", "EndTime", "SyncOutputTimestamps", "UpdateFrameRate" }, null, new[]{ typeof(global::Mediapipe.PacketThinnerCalculatorOptions.Types.ThinnerType) }, new pb::Extension[] { global::Mediapipe.PacketThinnerCalculatorOptions.Extensions.Ext }, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class PacketThinnerCalculatorOptions : pb::IMessage<PacketThinnerCalculatorOptions>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<PacketThinnerCalculatorOptions> _parser = new pb::MessageParser<PacketThinnerCalculatorOptions>(() => new PacketThinnerCalculatorOptions());
    private pb::UnknownFieldSet _unknownFields;
    private int _hasBits0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<PacketThinnerCalculatorOptions> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Mediapipe.PacketThinnerCalculatorReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PacketThinnerCalculatorOptions() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PacketThinnerCalculatorOptions(PacketThinnerCalculatorOptions other) : this() {
      _hasBits0 = other._hasBits0;
      thinnerType_ = other.thinnerType_;
      period_ = other.period_;
      startTime_ = other.startTime_;
      endTime_ = other.endTime_;
      syncOutputTimestamps_ = other.syncOutputTimestamps_;
      updateFrameRate_ = other.updateFrameRate_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PacketThinnerCalculatorOptions Clone() {
      return new PacketThinnerCalculatorOptions(this);
    }

    /// <summary>Field number for the "thinner_type" field.</summary>
    public const int ThinnerTypeFieldNumber = 1;
    private readonly static global::Mediapipe.PacketThinnerCalculatorOptions.Types.ThinnerType ThinnerTypeDefaultValue = global::Mediapipe.PacketThinnerCalculatorOptions.Types.ThinnerType.Async;

    private global::Mediapipe.PacketThinnerCalculatorOptions.Types.ThinnerType thinnerType_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.PacketThinnerCalculatorOptions.Types.ThinnerType ThinnerType {
      get { if ((_hasBits0 & 1) != 0) { return thinnerType_; } else { return ThinnerTypeDefaultValue; } }
      set {
        _hasBits0 |= 1;
        thinnerType_ = value;
      }
    }
    /// <summary>Gets whether the "thinner_type" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasThinnerType {
      get { return (_hasBits0 & 1) != 0; }
    }
    /// <summary>Clears the value of the "thinner_type" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearThinnerType() {
      _hasBits0 &= ~1;
    }

    /// <summary>Field number for the "period" field.</summary>
    public const int PeriodFieldNumber = 2;
    private readonly static long PeriodDefaultValue = 1L;

    private long period_;
    /// <summary>
    /// The period (in microsecond) specifies the temporal interval during which
    /// only a single packet is emitted in the output stream.  Has subtly different
    /// semantics depending on the thinner type, as follows.
    ///
    /// Async thinner: this option is a refractory period -- once a packet is
    /// emitted, we guarantee that no packets will be emitted for period ticks.
    ///
    /// Sync thinner: the period specifies a temporal interval during which
    /// only one packet is emitted.  The emitted packet is guaranteed to be
    /// the one closest to the center of the temporal interval (no guarantee on
    /// how ties are broken).  More specifically,
    ///   intervals are centered at start_time + i * period
    ///   (for non-negative integers i).
    /// Thus, each interval extends period/2 ticks before and after its center.
    /// Additionally, in the sync thinner any packets earlier than start_time
    /// are discarded and the thinner calls Close() once timestamp equals or
    /// exceeds end_time.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long Period {
      get { if ((_hasBits0 & 2) != 0) { return period_; } else { return PeriodDefaultValue; } }
      set {
        _hasBits0 |= 2;
        period_ = value;
      }
    }
    /// <summary>Gets whether the "period" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasPeriod {
      get { return (_hasBits0 & 2) != 0; }
    }
    /// <summary>Clears the value of the "period" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearPeriod() {
      _hasBits0 &= ~2;
    }

    /// <summary>Field number for the "start_time" field.</summary>
    public const int StartTimeFieldNumber = 3;
    private readonly static long StartTimeDefaultValue = 0L;

    private long startTime_;
    /// <summary>
    /// Packets before start_time and at/after end_time are discarded.
    /// Additionally, for a sync thinner, start time specifies the center of
    /// time invervals as described above and therefore should be set explicitly.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long StartTime {
      get { if ((_hasBits0 & 4) != 0) { return startTime_; } else { return StartTimeDefaultValue; } }
      set {
        _hasBits0 |= 4;
        startTime_ = value;
      }
    }
    /// <summary>Gets whether the "start_time" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasStartTime {
      get { return (_hasBits0 & 4) != 0; }
    }
    /// <summary>Clears the value of the "start_time" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearStartTime() {
      _hasBits0 &= ~4;
    }

    /// <summary>Field number for the "end_time" field.</summary>
    public const int EndTimeFieldNumber = 4;
    private readonly static long EndTimeDefaultValue = 0L;

    private long endTime_;
    /// <summary>
    /// and set to Timestamp::Min() for ASYNC type.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long EndTime {
      get { if ((_hasBits0 & 8) != 0) { return endTime_; } else { return EndTimeDefaultValue; } }
      set {
        _hasBits0 |= 8;
        endTime_ = value;
      }
    }
    /// <summary>Gets whether the "end_time" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasEndTime {
      get { return (_hasBits0 & 8) != 0; }
    }
    /// <summary>Clears the value of the "end_time" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearEndTime() {
      _hasBits0 &= ~8;
    }

    /// <summary>Field number for the "sync_output_timestamps" field.</summary>
    public const int SyncOutputTimestampsFieldNumber = 5;
    private readonly static bool SyncOutputTimestampsDefaultValue = true;

    private bool syncOutputTimestamps_;
    /// <summary>
    /// Whether the timestamps of packets emitted by sync thinner should
    /// correspond to the center of their corresponding temporal interval.
    /// If false, packets emitted using original timestamp (as in async thinner).
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool SyncOutputTimestamps {
      get { if ((_hasBits0 & 16) != 0) { return syncOutputTimestamps_; } else { return SyncOutputTimestampsDefaultValue; } }
      set {
        _hasBits0 |= 16;
        syncOutputTimestamps_ = value;
      }
    }
    /// <summary>Gets whether the "sync_output_timestamps" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasSyncOutputTimestamps {
      get { return (_hasBits0 & 16) != 0; }
    }
    /// <summary>Clears the value of the "sync_output_timestamps" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearSyncOutputTimestamps() {
      _hasBits0 &= ~16;
    }

    /// <summary>Field number for the "update_frame_rate" field.</summary>
    public const int UpdateFrameRateFieldNumber = 6;
    private readonly static bool UpdateFrameRateDefaultValue = false;

    private bool updateFrameRate_;
    /// <summary>
    /// If true, update the frame rate in the header, if it's available, to an
    /// estimated frame rate due to the sampling.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool UpdateFrameRate {
      get { if ((_hasBits0 & 32) != 0) { return updateFrameRate_; } else { return UpdateFrameRateDefaultValue; } }
      set {
        _hasBits0 |= 32;
        updateFrameRate_ = value;
      }
    }
    /// <summary>Gets whether the "update_frame_rate" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasUpdateFrameRate {
      get { return (_hasBits0 & 32) != 0; }
    }
    /// <summary>Clears the value of the "update_frame_rate" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearUpdateFrameRate() {
      _hasBits0 &= ~32;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as PacketThinnerCalculatorOptions);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(PacketThinnerCalculatorOptions other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ThinnerType != other.ThinnerType) return false;
      if (Period != other.Period) return false;
      if (StartTime != other.StartTime) return false;
      if (EndTime != other.EndTime) return false;
      if (SyncOutputTimestamps != other.SyncOutputTimestamps) return false;
      if (UpdateFrameRate != other.UpdateFrameRate) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (HasThinnerType) hash ^= ThinnerType.GetHashCode();
      if (HasPeriod) hash ^= Period.GetHashCode();
      if (HasStartTime) hash ^= StartTime.GetHashCode();
      if (HasEndTime) hash ^= EndTime.GetHashCode();
      if (HasSyncOutputTimestamps) hash ^= SyncOutputTimestamps.GetHashCode();
      if (HasUpdateFrameRate) hash ^= UpdateFrameRate.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (HasThinnerType) {
        output.WriteRawTag(8);
        output.WriteEnum((int) ThinnerType);
      }
      if (HasPeriod) {
        output.WriteRawTag(16);
        output.WriteInt64(Period);
      }
      if (HasStartTime) {
        output.WriteRawTag(24);
        output.WriteInt64(StartTime);
      }
      if (HasEndTime) {
        output.WriteRawTag(32);
        output.WriteInt64(EndTime);
      }
      if (HasSyncOutputTimestamps) {
        output.WriteRawTag(40);
        output.WriteBool(SyncOutputTimestamps);
      }
      if (HasUpdateFrameRate) {
        output.WriteRawTag(48);
        output.WriteBool(UpdateFrameRate);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (HasThinnerType) {
        output.WriteRawTag(8);
        output.WriteEnum((int) ThinnerType);
      }
      if (HasPeriod) {
        output.WriteRawTag(16);
        output.WriteInt64(Period);
      }
      if (HasStartTime) {
        output.WriteRawTag(24);
        output.WriteInt64(StartTime);
      }
      if (HasEndTime) {
        output.WriteRawTag(32);
        output.WriteInt64(EndTime);
      }
      if (HasSyncOutputTimestamps) {
        output.WriteRawTag(40);
        output.WriteBool(SyncOutputTimestamps);
      }
      if (HasUpdateFrameRate) {
        output.WriteRawTag(48);
        output.WriteBool(UpdateFrameRate);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (HasThinnerType) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) ThinnerType);
      }
      if (HasPeriod) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Period);
      }
      if (HasStartTime) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(StartTime);
      }
      if (HasEndTime) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(EndTime);
      }
      if (HasSyncOutputTimestamps) {
        size += 1 + 1;
      }
      if (HasUpdateFrameRate) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(PacketThinnerCalculatorOptions other) {
      if (other == null) {
        return;
      }
      if (other.HasThinnerType) {
        ThinnerType = other.ThinnerType;
      }
      if (other.HasPeriod) {
        Period = other.Period;
      }
      if (other.HasStartTime) {
        StartTime = other.StartTime;
      }
      if (other.HasEndTime) {
        EndTime = other.EndTime;
      }
      if (other.HasSyncOutputTimestamps) {
        SyncOutputTimestamps = other.SyncOutputTimestamps;
      }
      if (other.HasUpdateFrameRate) {
        UpdateFrameRate = other.UpdateFrameRate;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            ThinnerType = (global::Mediapipe.PacketThinnerCalculatorOptions.Types.ThinnerType) input.ReadEnum();
            break;
          }
          case 16: {
            Period = input.ReadInt64();
            break;
          }
          case 24: {
            StartTime = input.ReadInt64();
            break;
          }
          case 32: {
            EndTime = input.ReadInt64();
            break;
          }
          case 40: {
            SyncOutputTimestamps = input.ReadBool();
            break;
          }
          case 48: {
            UpdateFrameRate = input.ReadBool();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            ThinnerType = (global::Mediapipe.PacketThinnerCalculatorOptions.Types.ThinnerType) input.ReadEnum();
            break;
          }
          case 16: {
            Period = input.ReadInt64();
            break;
          }
          case 24: {
            StartTime = input.ReadInt64();
            break;
          }
          case 32: {
            EndTime = input.ReadInt64();
            break;
          }
          case 40: {
            SyncOutputTimestamps = input.ReadBool();
            break;
          }
          case 48: {
            UpdateFrameRate = input.ReadBool();
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the PacketThinnerCalculatorOptions message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      public enum ThinnerType {
        /// <summary>
        /// Asynchronous thinner, described below [default].
        /// </summary>
        [pbr::OriginalName("ASYNC")] Async = 1,
        /// <summary>
        /// Synchronous thinner, also described below.
        /// </summary>
        [pbr::OriginalName("SYNC")] Sync = 2,
      }

    }
    #endregion

    #region Extensions
    /// <summary>Container for extensions for other messages declared in the PacketThinnerCalculatorOptions message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Extensions {
      public static readonly pb::Extension<global::Mediapipe.CalculatorOptions, global::Mediapipe.PacketThinnerCalculatorOptions> Ext =
        new pb::Extension<global::Mediapipe.CalculatorOptions, global::Mediapipe.PacketThinnerCalculatorOptions>(288533508, pb::FieldCodec.ForMessage(2308268066, global::Mediapipe.PacketThinnerCalculatorOptions.Parser));
    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
