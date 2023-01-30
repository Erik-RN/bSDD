"""
This is a script to convert an Excel file made using the attached bSDD template to a properly structured bSDD JSON file. 

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

__license__ = "MIT"
__author__ = "Artur Tomczak"
__copyright__ = "Copyright (c) 2023 buildingSMART International Ltd."
__version__ = "1.0.0"
__email__ = "bsdd_support@buildingsmart.org"
"""

import sys
import json
from ast import literal_eval 
from copy import deepcopy
from JSON_templates import Domain, Classification, ClassificationRelation, AllowedValue
from JSON_templates import ClassificationProperty, Property, PropertyRelation, Material
import numpy as np
import pandas as pd


def load_excel(EXCEL_PATH):
    """Parses an excel file from path. Note: only works on provided template file.

    :param EXCEL_PATH: Path to an excel file 
    :type EXCEL_PATH: str
    :return: Dictionary of Pandas dataframes with parsed Excel data
    :rtype: dict
    """
    xls = pd.ExcelFile(EXCEL_PATH)
    excel={}
    excel['domain'] = pd.read_excel(xls, 'Domain', skiprows=5, usecols="C:Q", true_values="TRUE")
    excel['classification'] = pd.read_excel(xls, 'Classification', skiprows=5, usecols="C:AA", true_values="TRUE")
    excel['material'] = pd.read_excel(xls, 'Material', skiprows=5, usecols="C:AA", true_values="TRUE")
    excel['property'] = pd.read_excel(xls, 'Property', skiprows=5, usecols="C:AT", true_values="TRUE")
    excel['classificationproperty'] = pd.read_excel(xls, 'ClassificationProperty', usecols="C:T", skiprows=5, true_values="TRUE")
    excel['classificationrelation'] = pd.read_excel(xls, 'ClassificationRelation', usecols="C:G", skiprows=5, true_values="TRUE")
    excel['propertyvalue'] = pd.read_excel(xls, 'PropertyValue', skiprows=5, usecols="C:AT", true_values="TRUE")
    excel['propertyrelation'] = pd.read_excel(xls, 'PropertyRelation', skiprows=5, usecols="C:AT", true_values="TRUE")
    return excel

def jsonify(dataframe, json_part):
    """Transforms the input pandas dataframe to JSON only if a property exists in the template

    :param dataframe: Pandas dataframe with parsed Excel data
    :type dataframe: pd.DataFrame
    :param json_part: template dictinary from JSON_templates
    :type json_part: dict
    :return: Resultant list of dictionaries containing each row of the pandas table converted to appropriate dictionary
    :rtype: list
    """
    jsons = []
    # replace NANs
    dataframe = dataframe.astype(object).replace(np.nan, None)
    for index, row in dataframe.iterrows():
        new_part = deepcopy(json_part)
        for column_name, column_data in row.items():
            if column_name in new_part:
                # Convert date to: 2022-05-12T00:00:00+02:00
                if type(column_data) == pd._libs.tslibs.timestamps.Timestamp:
                    column_data = column_data.isoformat()
                elif type(column_data) == str:
                    if "[" in column_data and "]" in column_data:
                        content = literal_eval(column_data)
                        if isinstance(content, list):
                            column_data = content
                new_part[column_name] = column_data
            else:
                print(f"No such property as '{column_name}' in the JSON")
        jsons.append(new_part)
    return jsons

def clean_nones(value):
    """Recursively remove all None values from dictionaries and lists, and returns
    the result as a new dictionary or list.
    """
    if isinstance(value, list):
        return [clean_nones(x) for x in value if x is not None]
    elif isinstance(value, dict):
        return {
            key: clean_nones(val)
            for key, val in value.items()
            if val is not None
        }
    else:
        return value

def excel2bsdd(excel):
    """Goes through all dataframes and appends data to the desired JSON structure

    :param excel: Dictionary of Pandas dataframes from load_excel
    :type excel: dict
    :return: Resultant JSON structure
    :rtype: dict
    """
    bsdd_data = {}
    bsdd_data = jsonify(excel['domain'], Domain)[0]
    # append lists
    bsdd_data['Classifications'] = jsonify(excel['classification'], Classification)
    bsdd_data['Materials'] = jsonify(excel['material'], Material)
    bsdd_data['Properties'] = jsonify(excel['property'], Property)
    # find related object and append
    cls_props = jsonify(excel['classificationproperty'], ClassificationProperty)
    for cls_prop in cls_props:
        related = cls_prop['(Origin Classification Code)']
        cls_prop.pop("(Origin Classification Code)")
        next(item for item in bsdd_data['Classifications'] if item["Code"] == related)['ClassificationProperties'].append(cls_prop)

    cls_rels = jsonify(excel['classificationrelation'], ClassificationRelation)
    for cls_rel in cls_rels:
        related = cls_rel['(Origin Classification Code)']
        cls_rel.pop("(Origin Classification Code)")
        next(item for item in bsdd_data['Classifications'] if item["Code"] == related)['ClassificationRelations'].append(cls_rel)

    prop_vals = jsonify(excel['propertyvalue'], AllowedValue)
    for prop_val in prop_vals:
        related = prop_val['(Origin Property Code OR ClassificationProperty Code)']
        prop_val.pop("(Origin Property Code OR ClassificationProperty Code)")
        next(item for item in bsdd_data['Properties'] if item["Code"] == related)['AllowedValues'].append(prop_val)
        for cls in bsdd_data['Classifications']:
            next(item for item in cls if item["Code"] == related)['AllowedValues'].append(prop_val)

    prop_rels = jsonify(excel['propertyrelation'], PropertyRelation)
    for prop_rel in prop_rels:
        related = prop_rel['(Origin Property)']
        prop_rel.pop("(Origin Property)")
        next(item for item in bsdd_data['Properties'] if item["Code"] == related)['PropertyRelations'].append(prop_rel)
    
    return bsdd_data


if __name__ == "__main__":
    EXCEL_PATH = sys.argv[0]
    JSON_PATH = sys.argv[1]
    WITH_NULLS = sys.argv[2]

    excel = load_excel(EXCEL_PATH)
    bsdd_data = excel2bsdd(excel)

    if not WITH_NULLS:
        bsdd_data = clean_nones(bsdd_data)
    resultant_file = open(JSON_PATH, "w")
    json.dump(bsdd_data, resultant_file, indent = 2)
    resultant_file.close()