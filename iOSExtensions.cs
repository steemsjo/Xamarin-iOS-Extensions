public static class iOSExtensions
{
	#region UIView
	public static void SetX(this UIView v, nfloat x)
	{
		v.Frame = new CGRect(x, v.Frame.Y, v.Frame.Width, v.Frame.Height);
	}

	public static void SetY(this UIView v, nfloat y)
	{
		v.Frame = new CGRect(v.Frame.X, y, v.Frame.Width, v.Frame.Height);
	}

	public static void SetLocation(this UIView v, CGPoint point)
	{
		v.Frame = new CGRect(point, v.Frame.Size);
	}

	public static void SetLocation(this UIView v, nfloat x, nfloat y)
	{
		var p = new CGPoint(x, y);
		v.SetLocation(p);
	}

	public static void SetFrame(this UIView v, CGRect rect)
	{
		v.Frame = rect;
	}

	public static void SetFrame(this UIView v, CGPoint point, CGSize size)
	{
		var rect = new CGRect(point, size);
		v.Frame = rect;
	}

	public static void SetFrame(this UIView v, nfloat x, nfloat y, nfloat width, nfloat height)
	{
		var rect = new CGRect(x, y, width, height);
		v.Frame = rect;
	}

	public static void SetHeight(this UIView v, nfloat h)
	{
		v.SetFrame(v.Frame.X, v.Frame.Y, v.Frame.Width, h);
	}

	public static void SetWidth(this UIView v, nfloat w)
	{
		v.SetFrame(v.Frame.X, v.Frame.Y, w, v.Frame.Height);
	}

	public static nfloat GetViewsHeight(this UIView view)
	{
		return (nfloat)view.Subviews.Sum(sv => sv.Frame.Height);
	}

	public static void SetBorder(this UIView v, UIColor color, nfloat width, nfloat cornerRadius)
	{
		v.Layer.BorderColor = color.CGColor;
		v.Layer.BorderWidth = width;
		v.Layer.CornerRadius = cornerRadius;
	}

	public static void SetDebugTrace(this UIView v)
	{
		Random rnd = new Random((int)DateTime.Now.Ticks);
		var r = rnd.Next(50, 200);
		var g = rnd.Next(50, 200);
		var b = rnd.Next(50, 200);

		var color = UIColor.FromRGB(r, g, b);
		v.SetBorder(color, 1, 0);
	}

	public static void RemoveAllSubviews(this UIView v)
	{
		foreach(var view in v.Subviews)
		{
			view.RemoveFromSuperview();
		}
	}

	public static UIView GetFirstResponder(this UIView view)
	{
		if (view.IsFirstResponder)
		return view;

		foreach (var subView in view.Subviews)
		{
			var firstResponder = subView.FindFirstResponder();
			if (firstResponder != null)
			return firstResponder;
		}

		return null;
	}
	#endregion

	#region UITableView
	public static void ScrollToTop(this UITableView table, bool animated)
	{
		table.ScrollRectToVisible(new CGRect(0, 0, 1, 1), animated);
	}
	#endregion
	
	#region UIImage
	public static UIImage ScaleWithHeight(this UIImage image, nfloat Height)
    {
        var width = Height / image.Size.Height * image.Size.Width;
        return image.Scale(new CGSize(width, Height));
    }

    public static UIImage ScaleWithWidth(this UIImage image, nfloat Width)
    {
        var height = Width / image.Size.Width * image.Size.Height;
        return image.Scale(new CGSize(Width, height));
    }

    public static UIImage Blur(this UIImage image, float blurRadius = 25f)
	{
	  if (image != null)
	  {
	    // Create a new blurred image.
	    var imageToBlur = new CIImage (image);
	    var blur = new CIGaussianBlur ();
	    blur.Image = imageToBlur;
	    blur.Radius = blurRadius;
	    
	    var blurImage = blur.OutputImage;
	    var context = CIContext.FromOptions (new CIContextOptions { UseSoftwareRenderer = false });
	    var cgImage = context.CreateCGImage (blurImage, new RectangleF (new PointF (0, 0), image.Size));
	    var newImage = UIImage.FromImage (cgImage);
	    
	    // Clean up
	    imageToBlur.Dispose ();
	    context.Dispose ();
	    blur.Dispose ();
	    blurImage.Dispose ();
	    cgImage.Dispose ();
	    
	    return newImage;
	  }
	  return null;
	}
	#endregion

	#region General
	public static string VersionApp()
	{
		return NSBundle.MainBundle.InfoDictionary[new NSString("CFBundleShortVersionString")].ToString();
	}

	public static bool IsPortrait()
	{
		return UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.Portrait || UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.PortraitUpsideDown;
	}

	public static bool IsLandscape()
	{
		return !IsPortrait();
	}

	public static bool IsIPad()
	{
		return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad;
	}

	public static bool IsIPhone()
	{
		return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone;
	}
	#endregion
}