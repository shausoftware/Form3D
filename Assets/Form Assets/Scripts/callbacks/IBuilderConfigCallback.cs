using UnityEngine;
using System.Collections;

public interface IBuilderConfigCallback {

	void changeSelectedFormConfig(int index);

	void createNewBrush();

	void clearBrush();
}
