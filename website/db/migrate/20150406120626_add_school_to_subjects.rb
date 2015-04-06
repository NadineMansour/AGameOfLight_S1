class AddSchoolToSubjects < ActiveRecord::Migration
  def change
    add_column :subjects, :school, :string
  end
end
