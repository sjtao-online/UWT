using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace UWT.Templates.Models.Basics
{
    /// <summary>
    /// UWT唯一值生成逻辑
    /// </summary>
    public sealed class Uwtid : IComparable, IComparable<Uwtid>, IEquatable<Uwtid>
    {
        byte[] membuf = new byte[8];
        /// <summary>
        /// 空唯一值
        /// </summary>
        public static readonly Uwtid Empty;
        static UwtidConfig _config = new UwtidConfig()
        {
            TimeStampBits = 41,
            MachineBits = 8,
            IndexBits = 12,
            MachineId = 0
        };
        static DateTimeOffset TimeStampBeginOffset = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero);
        /// <summary>
        /// Uwtid配置<br/>
        /// 请参考IUwtidConfig配置说明
        /// </summary>
        public static IUwtidConfig Config => _config;
        /// <summary>
        /// UWT唯一值
        /// </summary>
        /// <param name="value">生成值</param>
        public Uwtid(UInt64 value)
        {
            if (value != 0)
            {
                var buf = BitConverter.GetBytes(value);
                for (int i = 0; i < 8; i++)
                {
                    membuf[i] = buf[8 - i - 1];
                }
            }
        }

        /// <summary>
        /// 生成唯一值
        /// </summary>
        /// <returns>生成新的Uwtid</returns>
        public static Uwtid NewUwtid()
        {
            return NewUwtid(Config);
        }

        /// <summary>
        /// 生成唯一值
        /// </summary>
        /// <param name="config">使用自定义配置，此配置应是重复使用</param>
        /// <returns>生成新的Uwtid</returns>
        public static Uwtid NewUwtid(IUwtidConfig config)
        {
            //  初始化变量
            const sbyte sizebuf = sizeof(ulong);
            sbyte tsb=0;
            sbyte mb =0;
            uint  mi = 0;
            ulong idx = 0;
            //  建锁赋值
            lock (config)
            {
                tsb = config.TimeStampBits;
                mb = config.MachineBits;
                mi = config.MachineId;
                //  编号递增
                config.UwtidIndex++;
                //  超范围回归
                idx = config.UwtidIndex % (ulong)Math.Pow(2, config.IndexBits);
            }
            //  计算时间差值
            TimeSpan timeSpan = DateTimeOffset.Now - TimeStampBeginOffset;
            //  转换为时间戳(毫秒)
            ulong uwtidvalue = (ulong)timeSpan.TotalMilliseconds;
            //  取余，取尾数，超出自动回归
            uwtidvalue = uwtidvalue % (ulong)Math.Pow(2, tsb);
            //  偏移到高位
            uwtidvalue = uwtidvalue << (sizebuf - tsb);
            //  机器Id偏移到中间位
            uwtidvalue += mi << (sizebuf - tsb - mb);
            //  低位使用序号
            uwtidvalue += idx;
            return new Uwtid(uwtidvalue);
        }

        /// <summary>
        /// 返回byte[]字节数组<br/>
        /// 8字节长度
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            return membuf;
        }

        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="timeStampBits">对应IUwtidConfig中TimeStampBits</param>
        /// <param name="machineBits">对应IUwtidConfig中MachineBits</param>
        /// <param name="indexBits">对应IUwtidConfig中IndexBits</param>
        /// <param name="machineId">对应IUwtidConfig中MachineId</param>
        /// <returns>为null成功，非null为失败</returns>
        public static string UpdateConfig(sbyte timeStampBits, sbyte machineBits, sbyte indexBits, uint machineId)
        {
            if (timeStampBits + machineBits + indexBits > 64)
            {
                return "总bits大于64";
            }
            if (timeStampBits < 40 || timeStampBits > 50)
            {
                return "timeStampBits超出可行域";
            }
            if (machineBits > 10)
            {
                return "machineBits超出可行域";
            }
            if (indexBits < 8 || indexBits > 13)
            {
                return "indexBits超出可行域";
            }
            if (machineId >= Math.Pow(2, machineBits))
            {
                return "机器Id大于机器容量";
            }
            _config.IndexBits = indexBits;
            _config.MachineBits = machineBits;
            _config.TimeStampBits = timeStampBits;
            _config.MachineId = machineId;   
            return null;
        }


        #region 重写的操作
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public int CompareTo([AllowNull] Uwtid other)
        {
            for (int i = 0; i < membuf.Length; i++)
            {
                int c = membuf[i].CompareTo(other.membuf[i]);
                if (c != 0)
                {
                    return c;
                }
            }
            return 0;
        }

        public int CompareTo(object obj)
        {
            if (obj is Uwtid)
            {
                return CompareTo((Uwtid)obj);
            }
            return -1;
        }

        public bool Equals([AllowNull] Uwtid other)
        {
            return other == this;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(membuf);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Uwtid)
            {
                return Equals((Uwtid)obj);
            }
            return false;
        }

        public override string ToString()
        {
            return BitConverter.ToString(membuf).Replace("-", "");
        }

        public static bool operator ==(Uwtid a, Uwtid b)
        {
            return Enumerable.SequenceEqual(a.membuf, b.membuf);
        }
        public static bool operator !=(Uwtid a, Uwtid b)
        {
            return Enumerable.SequenceEqual(a.membuf, b.membuf);
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        #endregion
    }
    /// <summary>
    /// UWT唯一值配置<br/>
    /// 所有位数相加应小于等于64
    /// </summary>
    public interface IUwtidConfig
    {
        /// <summary>
        /// 时间戳占用位数<br/>
        /// 默认41 取值区间[40,50]<br/>
        /// 此值越大回归年数越大{139年回归}<br/>
        /// 此值单一系统不应更改
        /// </summary>
        sbyte TimeStampBits { get; }
        /// <summary>
        /// 分布式机器Id所占位数<br/>
        /// 默认8 取值区间[0,10]<br/>
        /// 此值越大支持分布式机器越多{256台分布式}
        /// </summary>
        sbyte MachineBits { get; }
        /// <summary>
        /// 自增序号占用位数<br/>
        /// 应大于8 默认13<br/>
        /// 此值越大同一毫秒可生成唯一值越多{8192个}
        /// </summary>
        sbyte IndexBits { get; }
        /// <summary>
        /// 机器编号<br/>
        /// 默认0 取值区间[0,2^Machine)
        /// </summary>
        uint MachineId { get; }
        /// <summary>
        /// 初始序号默认为0
        /// </summary>
        ulong UwtidIndex { get; set; }
    }
    class UwtidConfig : IUwtidConfig
    {
        public sbyte TimeStampBits { get; set; }
        public sbyte MachineBits { get; set; }
        public sbyte IndexBits { get; set; }
        public uint MachineId { get; set; }
        public ulong UwtidIndex { get; set; }
    }
}
