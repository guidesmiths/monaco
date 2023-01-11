﻿namespace Monaco.Template.Backend.Domain.Model;

public class Image : File
{
	protected Image()
	{
	}

	public Image(Guid id,
				 string name,
				 string extension,
				 long size,
				 string contentType,
				 bool isTemp,
				 int height,
				 int width,
				 DateTime? dateTaken = null,
				 float? gpsLatitude = null,
				 float? gpsLongitude = null,
				 Image? thumbnail = null) : base(id, name, extension, size, contentType, isTemp)
	{
		Height = height;
		Width = width;
		DateTaken = dateTaken;
		GpsLatitude = gpsLatitude;
		GpsLongitude = gpsLongitude;
		Thumbnail = thumbnail;
	}

	public DateTime? DateTaken { get; private set; }
	public int Height { get; private set; }
	public int Width { get; private set; }
	public float? GpsLatitude { get; private set; }
	public float? GpsLongitude { get; private set; }

	public Guid? ThumbnailId { get; private set; }
	public virtual Image? Thumbnail { get; private set; }
}