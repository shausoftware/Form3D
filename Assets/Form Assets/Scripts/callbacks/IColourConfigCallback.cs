using UnityEngine;
using System.Collections;

public interface IColourConfigCallback  {

	void updateColourConfig(ColourConfiguration colourConfig);

	void setBackground(ColourConfiguration colourConfig);
}
