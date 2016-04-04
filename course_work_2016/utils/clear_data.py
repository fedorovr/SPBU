import pandas as pd
import os

cellid_str_report = 'Cell ID'
cellid_str_data = 'cellid'
activity_str_data = 'activity'

class Contain:
    def __init__(self, items):
        self.items = set(items)
    def __eq__(self, other):
        return other in self.items


report_df = pd.read_csv('../report_cleared.csv')
cellid_in_report = Contain(report_df[cellid_str_report])


for filename in os.listdir(os.getcwd()):
    if filename.endswith('.csv'):
        df = pd.read_csv(filename)
        was_rows = df.shape[0]

        # Drop column with activity description
        df.drop(activity_str_data, axis=1, inplace=True)
        # Drop rows with empty cellid
        df.dropna(subset=[cellid_str_data], inplace=True)
        # Drop rows with cellid not in report
        df = df[df[cellid_str_data] == cellid_in_report]

        print(was_rows - df.shape[0], " rows deleted of ", was_rows)
        # "converted" directory should exist in cwd for this to work
        df.to_csv('./converted/' + filename, index=False)
