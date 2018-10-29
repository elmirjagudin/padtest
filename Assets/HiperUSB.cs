using System;
using System.Text;
using System.Runtime.InteropServices;

public class HiperUSB
{
    [DllImport("libusb-1.0")]
    static extern int libusb_init(IntPtr ctx);

    [DllImport("libusb-1.0")]
    static extern void libusb_exit(IntPtr ctx);

    [DllImport("libusb-1.0")]
    static extern IntPtr libusb_open_device_with_vid_pid(IntPtr ctx, UInt16 vendor_id, UInt16 product_id);

    [DllImport("libusb-1.0")]
    static extern void libusb_close(IntPtr dev_handle);

    [DllImport("libusb-1.0")]
    static extern int libusb_set_configuration(IntPtr dev_handle, int configuration);

    [DllImport("libusb-1.0")]
    static extern int libusb_claim_interface(IntPtr dev_handle, int interface_number);

    [DllImport("libusb-1.0")]
    static extern int libusb_release_interface(IntPtr dev_handle, int interface_number);

    [DllImport("libusb-1.0")]
    static extern int libusb_bulk_transfer(IntPtr dev_handle, byte endpoint, byte[] data, int length, ref int transferred, int timeout);
    
    const UInt16 HIPER_VID = 0xC96;
    const UInt16 HIPER_PID = 0xCD;

    const int DEVICE_CONFIG = 1;
    const int DEVICE_INTERFACE = 0;
    const byte WRITE_ENDPOINT = 0x01;
    const byte READ_ENDPOINT = 0x81;

    const string ENABLE_NMEA_COMMAND = "em,/cur/term,/msg/nmea/GGA:.05\r\n";

    static IntPtr Device = IntPtr.Zero;
    static bool InterfaceClaimed = false;

    ///
    /// send the command to the device to enable NMEA string output
    ///
    static void EnableNMEA()
    {
        byte[] command = Encoding.ASCII.GetBytes(ENABLE_NMEA_COMMAND);
        int transferred = -1;

        var r = libusb_bulk_transfer(
            Device,
            WRITE_ENDPOINT,
            command,
            command.Length,
            ref transferred,
            0);

        Log.Msg("libusb_bulk_transfer r = {0} transferred = {1}", r, transferred);
    }

    public static void Init()
    {
        var r = libusb_init(IntPtr.Zero);
        Log.Msg("libusb_init r = {0}", r);
        if (r != 0)
        {
            throw new Exception("libusb_init failed");
        }

        Device = libusb_open_device_with_vid_pid(IntPtr.Zero, HIPER_VID, HIPER_PID);
        Log.Msg("Device {0}", Device);

        if (Device == IntPtr.Zero)
        {
            throw new Exception("libusb_open_device_with_vid_pid failed");
        }

        r = libusb_set_configuration(Device, DEVICE_CONFIG);
        Log.Msg("libusb_set_configuration r = {0}", r);
        if (r != 0)
        {
            throw new Exception("libusb_set_configuration failed");
        }

        r = libusb_claim_interface(Device, DEVICE_INTERFACE);
        Log.Msg("libusb_claim_interface r = {0}", r);
        if (r != 0)
        {
            throw new Exception("libusb_claim_interface failed");
        }
        InterfaceClaimed = true;

        EnableNMEA();
    }

    public static string ReadPos()
    {
        byte[] command = new byte[1024];
        int transferred = -1;

        var r = libusb_bulk_transfer(
            Device,
            READ_ENDPOINT,
            command,
            command.Length,
            ref transferred,
            0);

        string str = Encoding.ASCII.GetString(command);
        Log.Msg("libusb_bulk_transfer r = {0}  transferred = {1} str = '{2}'", r, transferred, str);
        return str;
    }

    public static void Cleanup()
    {
        if (InterfaceClaimed)
        {
            Log.Msg("releasing interface");
            libusb_release_interface(Device, DEVICE_INTERFACE);
            InterfaceClaimed = false;
        }

        if (Device != IntPtr.Zero)
        {
            Log.Msg("closing device");
            libusb_close(Device);
            Device = IntPtr.Zero;
        }

        Log.Msg("cleaning upp libusb");
        libusb_exit(IntPtr.Zero);
    }
}
